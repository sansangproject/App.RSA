using Microsoft.VisualBasic.FileIO;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsImage
    {
        public string Error = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strUser = "";
        public string PathFile = "";
        public string Location = "";

        public DataTable dt = new DataTable();
        public clsFunction Fn = new clsFunction();
        public clsMessage Message = new clsMessage();
        public dbConnection db = new dbConnection();
        public StoreConstant Store = new StoreConstant();
        public FieldConstant Fields = new FieldConstant();
        public ImageConstant Images = new ImageConstant();
        public OperationConstant Operation = new OperationConstant();

        public clsLog Log = new clsLog();

        public string[,] Parameter = new string[,] { };

        public void ShowImage(PictureBox pbImage, string Id = "", string Code = "", string Referrence = "")
        {
            try
            {
                pbImage.ImageLocation = GetLocationImage(Id, Code, Referrence);
            }
            catch
            {
                pbImage.ImageLocation = GetLocationImage(Id:Images.Null);
            }
        }

        public string GetLocationImage(string Id = "", string Code = "", string Referrence = "")
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Id},
                    {"@Code", Code},
                    {"@Status","0"},
                    {"@User",""},
                    {"@IsActive",""},
                    {"@IsDelete",""},
                    {"@Operation","S"},
                    {"@Referrence", Referrence},
                    {"@Path","0"},
                    {"@Name",""},
                    {"@Type",""},
                    {"@Size","0.00"},
                    {"@SizeName",""},
                    {"@Location",""},
                    {"@Byte",""},
                };

                db.Get(Store.ManageImage, Parameter, out Error, out dt);

                if (string.IsNullOrEmpty(Error))
                {
                    return Location = Fn.GetValue(dt, Fields.ImagePath);
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public void SelectImage()
        {
            OpenFileDialog Open = new OpenFileDialog();
            Open.Filter += "All Files *.*|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            Open.Filter += "|JPG Image Files(*.jpg;)|*.jpg";
            Open.Filter += "|JPEG Image Files(*.jpeg;)|*.jpeg";
            Open.Filter += "|GIF Image Files(*.gif;)|*.gif";
            Open.Filter += "|BMP Image Files(*.bmp;)|*.bmp";
            Open.Filter += "|PNG Image Files(*.png;)|*.png";

            ImageModel Photo = new ImageModel();
            GlobalVar.ImageDataList.Clear();

            if (Open.ShowDialog() == DialogResult.OK)
            {
                Photo.Code = Fn.GetCodes("1014", "", "Generated");
                Photo.Name = Path.GetFileNameWithoutExtension(Open.FileName);
                Photo.Type = Path.GetExtension(Open.FileName);
                Photo.Path = Open.FileName;

                if (new FileInfo(Open.FileName).Length > 1024)
                {
                    double ImageSize = new FileInfo(Open.FileName).Length;
                    Photo.Size = ImageSize / 1024;
                    Photo.Kind = "KB";
                }
                else
                {
                    Photo.Size = new FileInfo(Open.FileName).Length;
                    Photo.Kind = "Bytes";
                }

                Open.RestoreDirectory = true;
                GlobalVar.ImageDataList.Add(Photo);
            }
        }

        public void Show(PictureBox PictureBoxs, string Id)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Id},
                    {"@Code", ""},
                    {"@Name", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@Address", ""},
                    {"@TambolId", "0"},
                    {"@AmphoeId", "0"},
                    {"@ProvinceId", "0"},
                    {"@Postcode", ""},
                    {"@Phone", ""},
                    {"@Mobile", ""},
                    {"@Fax", ""},
                    {"@Email", ""},
                    {"@Line", ""},
                    {"@Web", ""},
                    {"@Manager", ""},
                    {"@OfficeHours",  ""},
                    {"@LogoId", "0"},
                    {"@TypeId", "0"},
                    {"@Remark",""},
                };

                db.Get(Store.ManageShop, Parameter, out Error, out dt);
                PictureBoxs.ImageLocation = dt.Rows[0]["Locations"].ToString();
            }
            catch
            {
                PictureBoxs.ImageLocation = GetLocationImage(Images.Null);
            }
        }

        public int GetBankLogo(PictureBox PictureBoxs, string Id)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Type", "BankLogo"},
                    {"@Value", Id},
                };

                db.Get(Store.FnGetBankLogo, Parameter, out Error, out dt);
                PictureBoxs.ImageLocation = dt.Rows[0]["Paths"].ToString();
                return Convert.ToInt32(dt.Rows[0]["Id"].ToString());
            }
            catch
            {
                PictureBoxs.ImageLocation = GetLocationImage(Images.Null);
                return 0;
            }
        }

        

       













































        public void Browse(PictureBox Picture, string Code, string Location, string User)
        {
            try
            {
                bool Stage = false;
                OpenFileDialog Open = new OpenFileDialog();
                Open.Filter += "All Files *.*|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
                Open.Filter += "|JPG Image Files(*.jpg;)|*.jpg";
                Open.Filter += "|JPEG Image Files(*.jpeg;)|*.jpeg";
                Open.Filter += "|GIF Image Files(*.gif;)|*.gif";
                Open.Filter += "|BMP Image Files(*.bmp;)|*.bmp";
                Open.Filter += "|PNG Image Files(*.png;)|*.png";

                ImageModel Model = new ImageModel();

                if (Open.ShowDialog() == DialogResult.OK)
                {
                    Model.Code = Fn.GetCodes("119", "", "Generated");
                    Model.Name = Path.GetFileNameWithoutExtension(Open.FileName);
                    Model.Type = Path.GetExtension(Open.FileName);
                    Model.Path = Open.FileName;
                    Model.Size = (new FileInfo(Open.FileName).Length / 1024);
                    Open.RestoreDirectory = true;
                }

                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", Model.Code},
                    {"@Referrence", Code},
                    {"@Path", Location},
                    {"@Name", Model.Name},
                    {"@Type", Model.Type},
                    {"@Size", Convert.ToString(Model.Size)},
                    {"@Location", Model.Path},
                    {"@Byte", ""},
                    {"@Status", "YI"},
                    {"@User", User},
                };

                if (ExistsImage(Code))
                {
                    db.Operations("Store.UpdateImage", Parameter, out Error);

                    if (string.IsNullOrEmpty(Error))
                    {
                        Stage = true;
                    }
                    else
                    {
                        Stage = false;
                    }
                }
                else
                {
                    db.Operations("Store.InsertImage", Parameter, out Error);

                    if (string.IsNullOrEmpty(Error))
                    {
                        Stage = true;
                    }
                    else
                    {
                        Stage = false;
                    }
                }

                if (Stage && ManageImage(Code, "", out PathFile))
                {
                    Image Img = new Bitmap(PathFile);
                    Picture.Image = Img.GetThumbnailImage(160, 150, null, new IntPtr());
                }
                else
                {
                    Picture.Image = null;
                }
            }
            catch
            {
                Picture.Image = null;
            }
            finally
            {

            }
        }

        public void DeleteImage(string Path)
        {
            try
            {
                if (File.Exists(Path))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.Delete(Path);
                }
            }
            catch
            {

            }
            finally
            {

            }
        }

        public void MoveImage(string OriginPath, string DestinationPath)
        {
            try
            {
                FileSystem.MoveFile(OriginPath, DestinationPath);
            }
            catch(Exception ex)
            {
                Log.WriteLogData("Class Image", "Move Image", "System", ex.Message);
            }
            finally
            {

            }
        }

        public void ShowDefault(PictureBox pbImage)
        {
            pbImage.ImageLocation = GetLocationImage(Images.Null);
        }

        public DataTable SearchImage(string ImageCode)
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ImageCode},
                    {"@Referrence", ""},
                    {"@Path", "0"},
                    {"@Name", ""},
                    {"@Type", ""},
                    {"@Size", ""},
                    {"@SizeName", ""},
                    {"@Location", ""},
                    {"@Byte", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                };

                db.Get("Store.SelectImage", Parameter, out Error, out dt);
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
        }

        public Image ConvertBaseToImage(string Base64)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(Base64);

                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    return Image.FromStream(ms, true);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ExistsImage(string Code)
        {
            try
            {
                string[,] SearchParam = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Referrence", Code},
                    {"@Path", "0"},
                    {"@Name", ""},
                    {"@Type", ""},
                    {"@Size", ""},
                    {"@SizeName", ""},
                    {"@Location", ""},
                    {"@Byte", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                };

                db.Get("Store.SelectImage", SearchParam, out Error, out dt);

                if (string.IsNullOrEmpty(Error))
                {
                    return Fn.GetRows(dt) > 0 ? true : false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }

        public bool ManageImage(string Code, string Referrence, out string PathFile)
        {
            try
            {
                string DestinationPath = "";
                string OriginalPath = "";
                PathFile = "";

                string[,] SearchParam = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", Code},
                    {"@Referrence", Referrence},
                    {"@Path", "0"},
                    {"@Name", ""},
                    {"@Type", ""},
                    {"@Size", ""},
                    {"@SizeName", ""},
                    {"@Location", ""},
                    {"@Byte", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                };

                db.Get("Store.SelectImage", SearchParam, out Error, out dt);
                DestinationPath = dt.Rows[0]["ImagePath"].ToString();
                OriginalPath = dt.Rows[0]["Location"].ToString();
                PathFile = DestinationPath;

                if (File.Exists(DestinationPath))
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    FileSystem.DeleteFile(DestinationPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    MoveImage(OriginalPath, DestinationPath);
                }
                else
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    MoveImage(OriginalPath, DestinationPath);
                }

                return true;
            }
            catch
            {
                PathFile = "";
                return false;
            }
        }

        public string GetPath(string PathCode)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id",""},
                    {"@Code", PathCode},
                    {"@Name",""},
                    {"@Location",""},
                    {"@Detail",""},
                    {"@Status",""},
                };

                db.Get("Store.SelectPath", Parameter, out Error, out dt);

                if (Fn.GetRows(dt) == 1)
                {
                    return Fn.GetValue(dt, Fields.Location);
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}

