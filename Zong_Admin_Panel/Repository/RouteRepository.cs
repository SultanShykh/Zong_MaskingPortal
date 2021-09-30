using Simple.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zong_Admin_Panel.Interaces;
using Zong_Admin_Panel.Models;

namespace Zong_Admin_Panel.Repository
{
    public class RouteRepository:IRoute
    {
      
            private dynamic PayDB;
            public RouteRepository()
            {
                this.PayDB = Database.OpenNamedConnection("MainDB");

            }

            public string Add(Routes route)
            {
                var result = PayDB.OutboxInsert(UserId: route.UserId, Sender: route.Sender, Reciver: route.Receiver, MsgData: route.MsgData, Route: route.Route, Operator: route.Operator);
                return "Added Successfully !!!";
            }

            //public IEnumerable<Routes> GetAll()
            //{
            //    List<Routes> users = new List<Routes>();
            //    var Result = PayDB.RouteGetAll();
            //    foreach (var v in Result)
            //    {
            //        users.Add(new Routes()
            //        {
            //            Id = v.Id,
            //            Telco = v.TelcoId,
            //            TelcoName=v.TelcoName,
            //            Network = v.Network,
            //            Route = v.Route,
            //            Operator = v.Operator,
            //            CreatedDatetime = v.CreateDatetime,

            //        });
            //    }
            //    return users;
            //}

            public IEnumerable<Routes> GetAll(int? Id)
            { 
            List<Routes> routes = new List<Routes>();
                var Result = PayDB.RoutesGet(Id);
                foreach (var v in Result)
                {
                    routes.Add(new Routes()
                    {
                        Id = v.Id,
                        Telco = v.TelcoId,
                        TelcoName = v.TelcoName,
                        Network = v.Network,
                        Route = Convert.ToInt32(v.Route).ToString(),
                        Operator = v.Operator,
                        CreatedDatetime = v.CreatedDatetime.ToString("dd-MMM-yyyy hh:mm"),

                    });
                }
                return routes;
            }


        }
    }
