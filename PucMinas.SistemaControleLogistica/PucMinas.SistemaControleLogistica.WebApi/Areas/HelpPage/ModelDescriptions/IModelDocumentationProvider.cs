using System;
using System.Reflection;

namespace PucMinas.SistemaControleLogistica.WebApi.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}