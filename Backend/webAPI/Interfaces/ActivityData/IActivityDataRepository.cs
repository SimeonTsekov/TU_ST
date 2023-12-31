﻿using webApi.Data.Models;

namespace webAPI.Interfaces.ActivityRepository
{
    public interface IActivityDataRepository
    {
        ActivityDataModel Create(ActivityDataModel newModel);
        ActivityDataModel Update(int activityDataId, ActivityDataModel updatedModel);
        List<ActivityDataModel> Get(int userId, string order, int count);
        ActivityDataModel GetById(int activityDataId);
        ActivityDataModel GetLatestActivityDataForTheCurrentUser();
        void Delete(int activityDataId);
    }
}
