﻿using System.Web.Http;
using ActivityAPI.Service;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Net;
using ActivityAPI.ActionFilter;
using System;

namespace ActivityAPI.Controllers
{
    /// <summary>
    /// 活動controller
    /// </summary>       
    [NotImplementFilter]
    public class ActivityController : BaseApiController
    {
        IActivityService service;
        public ActivityController(IActivityService _service)
        {
            service = _service;
        }

        /// <summary>
        /// 活動清單
        /// </summary>
        /// <returns></returns>         
        public IEnumerable<Models.Activity> GetActivitys()
        {
            return service.GetAll();
        }

        /// <summary>
        /// 取得單項活動
        /// </summary>
        /// <param name="Id">活動ID</param>
        /// <returns></returns>
        [ResponseType(typeof(Models.Activity))]           
        public Models.Activity GetActivity(int Id)
        {            
            var act = service.Get(Id);
            if(act != null)
            {
                return act;
            }
            else
            {
                var msg = "sorry, Activity with Id =" + Id + " not found";
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, msg));
            }
        }

        /// <summary>
        /// 新增活動
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(void))]  
        public HttpResponseMessage PostActivity([FromBody] Models.Activity activity)
        {    
            var addResult = service.Add(activity);
            if (addResult)
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                var msg = ""+activity.Name+" was created failed";
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, msg);
            }
        }

        /// <summary>
        /// 報名活動
        /// </summary>
        /// <param name="Id">活動ID</param>
        /// <returns></returns>
        [Route("Register/{Id:int}")]
        [HttpPost]
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage RegActivity(int Id)
        {
            HttpResponseMessage response;
           
            bool isfullbooked = service.IsFullyBooked(Id);

            if (isfullbooked)
            {
                response = Request.CreateResponse();
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Content = new StringContent("活動人數已額滿");
                return response;
            }
            else
            {
                service.RegActivity(Id);
                response = Request.CreateResponse();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = new StringContent("報名活動成功");
                return response;
            }
        }       

        [HttpPut]
        public HttpResponseMessage UpdateActivity(int Id, Models.Activity activity)
        {
            throw new NotImplementedException("Update Api is not Implemented");
        }

        [HttpDelete]
        public HttpResponseMessage DeleteActivity(int Id, Models.Activity activity)
        {
            throw new NotImplementedException("Delete Api is not Implemented");
        }

    }
}