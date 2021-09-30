using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Repository;

namespace Zong_Admin_Panel
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IZongRegions, ZongRegionsRepository>();
            container.RegisterType<ISender, SenderRepository>(); 
            container.RegisterType<IRoute, RouteRepository>();
            container.RegisterType<IZongRegionsUsers, ZongRegionUsers>();
            container.RegisterType<IHistory, HistoryRepository>();
            container.RegisterType<IRegionsHierarchy, RegionsHierarchyRepository>();
            container.RegisterType<IDashboard, DashBoardRepository>();
         
            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}