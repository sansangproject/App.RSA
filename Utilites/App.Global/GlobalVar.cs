/// ModifyDate Developmentcompany    Describe
/// 2017/10/17 TSS Sarawut           Create
/// (c) Copyright Denso All rights reserved.

using System.Collections.Generic;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Utilites.App.Global
{
    public class GlobalVar
    {
        /////////////////////////////////////////////////////////////////
        /// *** Don't Forget Public variable in Program.cs before *** ///
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Global value variable
        /// </summary>
        private static List<WaterModel> _WaterDataList;

        public static List<WaterModel> WaterDataList
        {
            get
            {
                return _WaterDataList;
            }
            set
            {
                _WaterDataList = value;
            }
        }

        private static List<ImageModel> _ImageDataList;

        /// <summary>
        /// Global value variable
        /// </summary>
        private static List<ElectricModel> _ElectricDataList;

        public static List<ElectricModel> ElectricDataList
        {
            get
            {
                return _ElectricDataList;
            }
            set
            {
                _ElectricDataList = value;
            }
        }

        public static List<ImageModel> ImageDataList
        {
            get
            {
                return _ImageDataList;
            }
            set
            {
                _ImageDataList = value;
            }
        }
    }
}