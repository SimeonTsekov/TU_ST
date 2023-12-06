﻿using webAPI.DTOs.Request;

namespace webAPI.DTOs.Response
{
    public class ActivityResponse
    {
        public int ActivityDataId { get; set; }
        public int UserId { get; set; }
        public int Workouts { get; set; }
        public int DailySteps { get; set; }
        public float DailyDistance { get; set; }
        public float DailyEnergyBurned { get; set; }
    }
}