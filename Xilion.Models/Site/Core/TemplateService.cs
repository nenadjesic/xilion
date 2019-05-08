using System;
using System.IO;
using System.Text.RegularExpressions;
using Xilion.Models.Site.Data;

namespace Xilion.Models.Site.Core
{
    public class TemplateService
    {
        private readonly IPageTemplateRepository _templateRepository;

        public TemplateService(IPageTemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public PageTemplate Import(string title, System.IO.Stream input)
        {
            string parsed;
            using (var reader = new StreamReader(input))
            {
                parsed = reader.ReadToEnd();
            }
            return Import(title, parsed);
        }

        public PageTemplate Import(string title, string input)
        {
            string content = input; // ParseRazorLayout(input);
            var template = new PageTemplate
                               {
                                   Title = title,
                                   Content = content
                               };
            _templateRepository.Save(template);
            return template;
        }

        public string ParseTemplate(string content)
        {
            string result = ParseWebFormMaster(content);
            result = ParseWebFormMaster(result);
            return result;
        }

        public static string ParseRazorLayout(string input)
        {
            string content = input.Replace("@RenderBody()", "@RenderSection(\"Body\", true)");
            const string cmsWidget = @"@Html.RenderCmsWidgets(""{0}"", ViewContext)";
            string r = @"{0}\(""(.*)"".*\);?";
            //Add something to match
            string renderSectionRegex = string.Format(r, "@RenderSection");
            var regex = new Regex(renderSectionRegex, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(content);
            foreach (Match match in matches)
            {
                string section = match.Value;
                string name = match.Groups[1].Value;
                content = content.Replace(match.Value, string.Format(cmsWidget, name));
            }
            return content;
        }

        public string ParseWebFormMaster(string input)
        {
            string content = input;
            var regex2 =
                new Regex(
                    "<[\\w]+:ContentPlaceHolder\\s+(runat\\s*=\\s*\"\\s*server\\s*\"\\s*)?id=\\s*\"(?<id>[^\"]+)\"\\s+(runat\\s*=\\s*\"\\s*server\\s*\"\\s*)?\\s*(/>|(>\\s*(?<content>[\\w\\W]*?)</[\\w]+:ContentPlaceHolder\\s*>))",
                    RegexOptions.IgnoreCase);
            foreach (Match match in regex2.Matches(content))
            {
                content = content.Replace(match.Value,
                                          "@Html.RenderCmsWidgets(\"" + match.Groups["id"].Value + "\", ViewContext)");
            }
            return content;
        }

        public PageTemplate Get(long id)
        {
            return _templateRepository.GetById(id);
        }
    }
}