using FootballOracle_Data;
using FootballOracle.Models;
using FootballOracle.Models.models;
using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace FootballOracle.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IChampionshipService championshipService;
        private readonly ITeamService teamService;

        public ArticleController(IArticleService ArticleService, ICommentService commentService,
            IChampionshipService championshipService, ITeamService teamService)
        {
            this.articleService = ArticleService;
            this.commentService = commentService;
            this.championshipService = championshipService;
            this.teamService = teamService;
        }

        public ActionResult News(Guid id)
        {
            var article = this.articleService.GetById(id);
            var otherNewsDB = this.articleService.GetLatestNewsForChampionship(article.ChampionshipId, id);
            var ArticlesForTeam = new List<NewsModel>();
            var Comments = new List<CommentModel>();
            var matches = new List<PlayedMatchModel>();
            int tagCount = article.Tags.Count();

            if (tagCount == 1)
            {
                this.articleService.GetLatestNewsForTeam(article.Tags.FirstOrDefault().TeamId, id).Where(x => x.Id != id).Take(4).ToList()
                    .ForEach(x =>
                                    {
                                        ArticlesForTeam.Add(new NewsModel()
                                        {
                                            Id = x.Id,
                                            ImageSrc = x.Image,
                                            Title = x.Title
                                        });
                                    });
            }
            else if (tagCount == 2)
            {
                this.articleService.GetLatestNewsForTeam(article.Tags.FirstOrDefault().TeamId, id).Where(x => x.Id != id).Take(2).ToList()
                    .ForEach(x =>
                    {
                        ArticlesForTeam.Add(new NewsModel()
                        {
                            Id = x.Id,
                            ImageSrc = x.Image,
                            Title = x.Title
                        });
                    });

                this.articleService.GetLatestNewsForTeam(article.Tags.LastOrDefault().TeamId, id).Where(x => x.Id != id).Take(2).ToList()
                    .ForEach(x =>
                    {
                        ArticlesForTeam.Add(new NewsModel()
                        {
                            Id = x.Id,
                            ImageSrc = x.Image,
                            Title = x.Title
                        });
                    });
            }

            this.commentService.GetCommentsForArticle(id).ToList().ForEach(x =>
            {
                var db = ApplicationDbContext.Create();
                string currentid = x.Id.ToString();
                string author = db.Users.Where(y => y.Id == currentid).Select(y => y.UserName).FirstOrDefault();
                Comments.Add(new CommentModel()
                {
                    Author = author,
                    Content = x.Description,
                    Date = x.Date
                });
            });

            this.championshipService.GetPlayedMatchByChampionshipId(article.ChampionshipId).ToList().ForEach(x =>
            {
                var homeTeamName = this.teamService.FindTeamNameById(x.HomeTeam);
                var awayTeamName = this.teamService.FindTeamNameById(x.AwayTeam);

                matches.Add(new PlayedMatchModel()
                {
                    HomeTeam = homeTeamName,
                    AwayTeam = awayTeamName,
                    AwayGoals = x.AwayGoals,
                    HomeGoals = x.HomeGoals,
                    Date = x.Date,
                    Id = x.Id
                });
            });

            var otherNews = new List<NewsModel>();

            foreach (var otherNew in otherNewsDB)
            {
                otherNews.Add(new NewsModel()
                {
                    Id = otherNew.Id,
                    ImageSrc = otherNew.Image,
                    Title = otherNew.Title
                });
            }

            var model = new NewsViewModel()
            {
                Content = article.Content,
                Title = article.Title,
                Date = article.Date,
                ImageSrc = article.Image,
                Views = article.Viewing,
                OtherNews = otherNews,
                MoreForThisArticle = ArticlesForTeam,
                Comments = Comments,
                Matches = matches,
                ChampionshipId = article.ChampionshipId,
                ArticleId = id
            };

            if (this.Session[id.ToString()] == null)
            {
                this.articleService.updateViewsById(id);
                this.Session.Add(id.ToString(), "imago");
            }

            return this.View(model);
        }

        public ActionResult Carousel()
        {
            List<CarouselViewModel> viewModel = new List<CarouselViewModel>();
            var articles = this.articleService.GetLatestFour();
            foreach (var article in articles)
            {
                viewModel.Add(new CarouselViewModel()
                {
                    Id = article.Id,
                    ImageSrc = article.Image,
                    Title = article.Title
                });
            }

            return this.PartialView("CarouselPartial", viewModel);
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel comment)
        {
            var model = new CommentsViewModel()
            {
                Comment = new List<CommentPartialModel>()
            };

            if (ModelState.IsValid)
            {
                var commentDb = new Comment()
                {
                    AccountId = Guid.Parse(User.Identity.GetUserId()),
                    ArticleId = comment.articleId,
                    Date = DateTime.Now,
                    Description = comment.Content,
                    Id = Guid.NewGuid()
                };

                this.commentService.AddComment(commentDb);
            }

            //Fix this :@
            var db = ApplicationDbContext.Create();

            this.commentService.GetCommentsForArticle(comment.articleId).ToList().ForEach(x =>
            {
                string currentId = x.AccountId.ToString();
                var user = db.Users.Where(y => y.Id == currentId).Select(y => y.UserName).FirstOrDefault();
                model.Comment.Add(new CommentPartialModel()
                {
                    Author = user,
                    AuthorId = x.AccountId,
                    Content = x.Description,
                    Date = x.Date
                });
            });

            return this.PartialView("CommentsPartial", model);
        }

        public ActionResult CommentsPartial(Guid id)
        {
            //Fix this :@
            var db = ApplicationDbContext.Create();

            var model = new CommentsViewModel()
            {
                Comment = new List<CommentPartialModel>()
            };

            this.commentService.GetCommentsForArticle(id).ToList().ForEach(x =>
            {
                string currentId = x.AccountId.ToString();
                var user = db.Users.Where(y => y.Id == currentId).Select(y => y.UserName).FirstOrDefault();
                model.Comment.Add(new CommentPartialModel()
                {
                    Author = user,
                    AuthorId = x.AccountId,
                    Content = x.Description,
                    Date = x.Date
                });
            });

            return this.PartialView("CommentsPartial", model);
        }
    }
}