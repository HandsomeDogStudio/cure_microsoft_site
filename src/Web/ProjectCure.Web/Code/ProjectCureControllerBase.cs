using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCureData;

namespace ProjectCure.Web.Code
{
    public class ProjectCureControllerBase : Controller
    {
        private readonly IRepository _repository;

        protected ProjectCureControllerBase(IRepository repository)
        {
            _repository = repository;
        }

        protected IRepository Repository
        {
            get { return _repository; }
        }
    }
}