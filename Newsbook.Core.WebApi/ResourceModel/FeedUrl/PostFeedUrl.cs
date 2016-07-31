using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.ResourceModel.FeedUrl
{
    [Validator(typeof(PostFeedUrlValidator))]
    public class PostFeedUrl : ResourceModelBase
    {
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Url { get; set; }
    }

    public class PostFeedUrlValidator : AbstractValidator<PostFeedUrl>
    {
        public PostFeedUrlValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("O Titulo não pode estar em branco.")
                                        .Length(1, 100).WithMessage("O Titulo deve ter entre 1 e 100 caracteres.");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("A Descricao não pode estar em branco.");

            RuleFor(x => x.Url).NotEmpty().WithMessage("A Url não pode estar em branco.");
        }
    }
}