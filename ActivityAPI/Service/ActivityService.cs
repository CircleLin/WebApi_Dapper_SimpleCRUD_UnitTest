﻿using ActivityAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivityAPI.Service
{
    public class ActivityService : IActivityService
    {
        IActivityRepository repo;

        public ActivityService(IActivityRepository _repo)
        {
            repo = _repo;
        }

       /// <summary>
       /// 取得目前報名人數
       /// </summary>
       /// <param name="activityId">活動ID</param>
       /// <returns></returns>
        public int CheckRegCountNow(int activityId)
        {
            return repo.GetActivity(activityId).RegisterCount;
        }

        /// <summary>
        /// 活動人數是否額滿
        /// </summary>
        /// <param name="activityId">活動ID</param>
        /// <returns>true額滿，false未額滿</returns>
        public bool IsFullyBooked(int activityId)
        {
            var act = repo.GetActivity(activityId);
            return act.RegisterCount >= act.Count? true : false;
        }

        /// <summary>
        /// 新增活動
        /// </summary>
        /// <param name="activity">活動物件</param>
        public bool Add(Models.Activity activity)
        {
            var result = repo.Add(activity);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 更新活動內容
        /// </summary>
        /// <param name="activity"></param>
        public bool Update(Models.Activity activity)
        {
            var result = repo.Update(activity);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 刪除活動
        /// </summary>
        /// <param name="activity"></param>
        public bool Delete(int Id)
        {            
            var result = repo.Delete(Id);
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 取得所有活動資訊
        /// </summary>
        /// <returns></returns>
        public List<Models.Activity> GetAll()
        {
            return repo.GetAllActivity();
        }

        /// <summary>
        /// 取得一項活動資訊
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Models.Activity Get(int activityId)
        {
            return repo.GetActivity(activityId);
        }

        /// <summary>
        /// 報名活動
        /// </summary>
        /// <param name="activityId">活動ID</param>
        /// <returns></returns>
        public void RegActivity(int activityId)
        {
            var act = this.Get(activityId);
            act.RegisterCount += 1;
        }
    }
}