﻿using Assignment.Models;
using Assignment.Models.EditModel;
using Assignment.Models.ViewModel;
using AutoMapper;

namespace Assignment.Profiles
{
    public class CustomProfile : Profile
    {
       public CustomProfile() {
            CreateMap<Subject, SubjectEditModel>().ReverseMap();
            CreateMap<Subject, SubjectViewModel>().ReverseMap();
            CreateMap<Room, RoomEditModel>().ReverseMap();
            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Class, ClassEditModel>().ReverseMap();
            CreateMap<Class, ClassViewModel>().ReverseMap();
            CreateMap<Teacher, TeacherEditModel>().ReverseMap();
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();
            CreateMap<Timetable, TimeTableEditModel>().ReverseMap();    
        }
    }
}
