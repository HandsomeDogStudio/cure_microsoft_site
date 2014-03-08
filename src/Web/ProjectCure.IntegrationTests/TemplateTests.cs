using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCureData;

namespace ProjectCure.IntegrationTests
{
	[TestClass]
	public class TemplateTests
	{
		[TestMethod]
		public void RetrieveTemplates()
		{
			var repo = new Repository();
			var template = repo.GetTemplateByName("Test Template");
			Assert.AreEqual("Template Subject", template.TemplateSubject);
			Assert.AreEqual("Template Text", template.TemplateText);
		}
	}
}
