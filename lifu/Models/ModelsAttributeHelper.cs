using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace lifu.Models
{
    //http://msdn.microsoft.com/zh-tw/library/system.componentmodel.dataannotations.aspx
    public class ModelsAttributeHelper<TModel> where TModel : class
    {
        #region -- GetDisplayName --



        /// <summary>

        /// Gets the display name.

        /// </summary>

        /// <param name="propertyName">Name of the property.</param>

        /// <returns></returns>

        public static string GetDisplayName(string propertyName)
        {

            Type type = typeof(TModel);

            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();

                    }
                }
            }
            return (attr != null) ? attr.Name : String.Empty;

        }

        #endregion

        #region -- GetDisplayName --



        /// <summary>

        /// Gets the MaxLength.

        /// </summary>

        /// <param name="propertyName">MaxLength of the property.</param>

        /// <returns></returns>

        public static string GetMaxLength(string propertyName)
        {

            Type type = typeof(TModel);

            MaxLengthAttribute attr;
            attr = (MaxLengthAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(MaxLengthAttribute), true).SingleOrDefault();

            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (MaxLengthAttribute)property.GetCustomAttributes(typeof(MaxLengthAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.Length.ToString() : String.Empty;

        }

        #endregion



        public static bool IsKey(string propertyName)
        {

            Type type = typeof(TModel);

            KeyAttribute attr;
            attr = (KeyAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(KeyAttribute), true).SingleOrDefault();

            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (KeyAttribute)property.GetCustomAttributes(typeof(KeyAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? true : false;

        }

        public static bool IsRequired(string propertyName)
        {

            Type type = typeof(TModel);

            RequiredAttribute attr;
            attr = (RequiredAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(RequiredAttribute), true).SingleOrDefault();

            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? true : false;

        }

        public static string GetDataTypeName(string propertyName)
        {
            Type type = typeof(TModel);
            PropertyInfo info = GetProperty(type, propertyName);
            return (info != null) ? info.PropertyType.Name : String.Empty;


        }

        private static PropertyInfo GetProperty(Type type, string propName)
        {

            try
            {

                PropertyInfo[] infos = type.GetProperties();

                if (infos == null)
                {

                    return null;

                }

                foreach (PropertyInfo info in infos)
                {

                    if (propName.ToLower().Equals(info.Name.ToLower()))
                    {

                        return info;

                    }

                }

            }

            catch (Exception ex)
            {

                return null;

                throw ex;

            }

            return null;

        }

        public static List<ModelPropertyInfo> GetModelPropertyInfo()
        {
            Type type = typeof(TModel);

            var infos = type.GetProperties();
            List<ModelPropertyInfo> list = new List<ModelPropertyInfo>();
            foreach (var prop in infos)
            {
                Type underlyingType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (prop.PropertyType.Name.IndexOf("ICollection")==-1)
                {
                    list.Add(new ModelPropertyInfo
                                           {
                                               Name = prop.Name,
                                               MaxLength = GetMaxLength(prop.Name),
                                               IsRequired = IsRequired(prop.Name),
                                               IsKey = IsKey(prop.Name),
                                               DisplayName = GetDisplayName(prop.Name),
                                               DataTypeName = underlyingType.Name
                                           });
                }


            }
            return list;

        }
    }

    public class ModelPropertyInfo
    {
        [Display(Name = "欄位名稱")]
        public string Name { set; get; }

        [Display(Name = "型別")]
        public string DataTypeName { set; get; }
        
        [Display(Name = "主索引")]
        public bool IsKey { set; get; }

        [Display(Name = "允許null")]
        public bool IsRequired { set; get; }

        [Display(Name = "最大長度")]
        public string MaxLength { set; get; }

        [Display(Name = "說明")]
        public string DisplayName { set; get; }
    }
}