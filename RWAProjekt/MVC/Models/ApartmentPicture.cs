﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ApartmentPicture
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string Path { get; set; }
        public string Base64Content { get; set; }
        public string Name { get; set; }
        public bool IsRepresentative { get; set; }

        public ApartmentPicture()
        {
        }

        public ApartmentPicture(int apartmentId, string path, string base64Content, string name, bool isRepresentative)
        {
            ApartmentId = apartmentId;
            Path = path;
            Base64Content = base64Content;
            Name = name;
            IsRepresentative = isRepresentative;
        }
    }
}