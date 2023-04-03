﻿using TrackingAssistantAPI.Shared;
using TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.DTOs;

namespace TrackingAssistantAPI.WorkoutTracker.WorkoutPlanExercise.Messages
{
    public class GetWorkoutPlanWithExercisesResponse : ServiceResponseBase
    {
        public WorkoutPlanExerciseDto? WorkoutPlanExercise { get; set; }
        public GetWorkoutPlanWithExercisesResponse() : base() { }
        public GetWorkoutPlanWithExercisesResponse(Exception ex) : base(ex) { }
    }
}
