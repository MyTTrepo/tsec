using System;

namespace ConsoleApplication1
{
    class UCStepInstruments
    {
        private bool isVisual = true;
        public bool UpdateInstruments()
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            Settings settings = new Settings();
            string str1 = FileService.ReadVersionFileContent();
            if (this.isVisual)
            {
                if (!StaticData.Version.Equals(str1) && StaticData.Instruments.Count != 0)
                {
                    /*
                    int num = (int)new ConfirmationForm("ویرایش جدید نرم افزار نصب گردید. بانک اطلاعاتی قیمت ها  که توسط نرم افزار \nویرایش قبلی ساخته شده موجود می باشد. در صورتی که با داده های قبلی مشکل دارید\n( و یا کامل نیست ) اطلاعات را با انتخاب دکمه «بلی» حذف کنید.\nدر غیر اینصورت کلید «خیر» را انتخاب کنید.\nتوجه : شما در هر زمان که مایل باشید می توانید بانک اطلاعاتی را از طریق کلید \n«پاک کردن اطلاعات» در بخش تنظیمات حذف کرده و مجددا داده های جدید را دریافت کنید.\n", "شما می توانید در محیط Command Prompt دستور TseClient.exe fast را اجرا کنید\nدر اینصورت کلیه عملیات بروزرسانی و ایجاد فایلهای خروجی به صورت خودکار انجام خواهد شد.\n\nهمچنین امکان ایجاد خروجی تعدیل شده فقط با استفاده از افزایش سرمایه\n به امکانات نرم افزار اضافه شده است").ShowDialog();
                    if (MainForm.ConfirmationResult)
                    {
                        FileService.EraseCurrentFiles();
                        settings.LastInstrumentReceiveDate = 0;
                        settings.Save();
                        StaticData.Instruments.Clear();
                    }
                    FileService.WriteVersionFileContent(StaticData.Version);
                    */
                }
                else if (StaticData.Instruments.Count == 0)
                    FileService.WriteVersionFileContent(StaticData.Version);
            }
            if (Utility.ConvertDateTimeToGregorianInt(DateTime.Now) == settings.LastInstrumentReceiveDate)
            {
                if (Utility.ConvertDateTimeToGregorianInt(DateTime.Now) != 20140217)
                    return true;
            }
            try
            {
                int lastDEven = 0;
                foreach (InstrumentInfo instrument in StaticData.Instruments)
                {
                    if (instrument.DEven > lastDEven)
                        lastDEven = instrument.DEven;
                }
                long lastId = 0;
                foreach (TseShareInfo tseShare in StaticData.TseShares)
                {
                    if (tseShare.Idn > lastId)
                        lastId = tseShare.Idn;
                }
                //string str2 = ServerMethods.InstrumentAndShare(lastDEven, lastId);
                string str2 = "aaaa@bbbb";
                string str3 = str2.Split('@')[0];
                if (!string.IsNullOrEmpty(str3))
                {
                    if (str3.Equals("*"))
                    {
                        if (this.isVisual)
                        {
                            //this.lblUpdateError.Text = "بروز رسانی اطلاعات در حد فاصل ساعت هشت صبح تا یک بعد از ظهر روزهای شنبه تا چهارشنبه امکان پذیر نمی باشد. \nجهت مشاهده لیست فعلی نمادها روی دکمه مرحله بعد کلیک کنید.";
                        }
                        return false;
                    }
                    string str4 = str3;
                    char[] chArray = new char[1] { ';' };
                    foreach (string str5 in str4.Split(chArray))
                    {
                        string[] row = str5.Split(',');
                        int index = StaticData.Instruments.FindIndex((Predicate<InstrumentInfo>)(p => p.InsCode == Convert.ToInt64(row[0])));
                        if (index < 0)
                        {
                            StaticData.Instruments.Add(new InstrumentInfo()
                            {
                                InsCode = Convert.ToInt64(row[0].ToString()),
                                InstrumentID = row[1].ToString(),
                                LatinSymbol = row[2].ToString(),
                                LatinName = row[3].ToString(),
                                CompanyCode = row[4].ToString(),
                                Symbol = row[5].ToString(),
                                Name = row[6].ToString(),
                                CIsin = row[7].ToString(),
                                DEven = Convert.ToInt32(row[8].ToString()),
                                Flow = Convert.ToByte(row[9].ToString()),
                                LSoc30 = row[10].ToString(),
                                CGdSVal = row[11].ToString(),
                                CGrValCot = row[12].ToString(),
                                YMarNSC = row[13].ToString(),
                                CComVal = row[14].ToString(),
                                CSecVal = row[15].ToString(),
                                CSoSecVal = row[16].ToString(),
                                YVal = row[17].ToString()
                            });
                        }
                        else
                        {
                            StaticData.Instruments[index].InstrumentID = row[1].ToString();
                            StaticData.Instruments[index].LatinSymbol = row[2].ToString();
                            StaticData.Instruments[index].LatinName = row[3].ToString();
                            StaticData.Instruments[index].CompanyCode = row[4].ToString();
                            StaticData.Instruments[index].Symbol = row[5].ToString();
                            StaticData.Instruments[index].Name = row[6].ToString();
                            StaticData.Instruments[index].CIsin = row[7].ToString();
                            StaticData.Instruments[index].DEven = Convert.ToInt32(row[8].ToString());
                            StaticData.Instruments[index].Flow = Convert.ToByte(row[9].ToString());
                            StaticData.Instruments[index].LSoc30 = row[10].ToString();
                            StaticData.Instruments[index].CGdSVal = row[11].ToString();
                            StaticData.Instruments[index].CGrValCot = row[12].ToString();
                            StaticData.Instruments[index].YMarNSC = row[13].ToString();
                            StaticData.Instruments[index].CComVal = row[14].ToString();
                            StaticData.Instruments[index].CSecVal = row[15].ToString();
                            StaticData.Instruments[index].CSoSecVal = row[16].ToString();
                            StaticData.Instruments[index].YVal = row[17].ToString();
                        }
                    }
                    FileService.WriteInstruments();
                    settings.LastInstrumentReceiveDate = Utility.ConvertDateTimeToGregorianInt(DateTime.Now);
                    settings.Save();
                }
                string str6 = str2.Split('@')[1];
                if (!string.IsNullOrEmpty(str6))
                {
                    string str4 = str6;
                    char[] chArray = new char[1] { ';' };
                    foreach (string str5 in str4.Split(chArray))
                    {
                        string[] row = str5.Split(',');
                        int index = StaticData.TseShares.FindIndex((Predicate<TseShareInfo>)(p => p.Idn == Convert.ToInt64(row[0])));
                        if (index < 0)
                        {
                            StaticData.TseShares.Add(new TseShareInfo()
                            {
                                Idn = Convert.ToInt64(row[0].ToString()),
                                InsCode = Convert.ToInt64(row[1].ToString()),
                                DEven = Convert.ToInt32(row[2].ToString()),
                                NumberOfShareNew = Convert.ToDecimal(row[3].ToString()),
                                NumberOfShareOld = Convert.ToDecimal(row[4].ToString())
                            });
                        }
                        else
                        {
                            StaticData.TseShares[index].InsCode = Convert.ToInt64(row[1].ToString());
                            StaticData.TseShares[index].DEven = Convert.ToInt32(row[2].ToString());
                            StaticData.TseShares[index].NumberOfShareNew = Convert.ToDecimal(row[3].ToString());
                            StaticData.TseShares[index].NumberOfShareOld = Convert.ToDecimal(row[4].ToString());
                        }
                    }
                    FileService.WriteTseShares();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The magic number in GZip header is not correct") && settings.EnableDecompression)
                {
                    settings.EnableDecompression = false;
                    settings.Save();
                    if (this.isVisual)
                        //this.owner.TabSwitcher(Tabs.Instruments);
                    return false;
                }
                if (this.isVisual)
                    //this.lblUpdateError.Text += ServerMethods.LogError("Instrument", ex);
                try
                {
                    /*
                    if (FileService.LogErrorFile("[ Instrument (" + StaticData.Version + ") (compression:" + settings.EnableDecompression.ToString() + ") ] " + ex.Message + "(" + (ex.InnerException != null ? ex.InnerException.Message ?? "" : "") + ")") == -1)
                    {
                        if (this.isVisual)
                        {
                            int num = (int)MessageBox.Show("مقدار فیلد محل ذخیره فایل ها صحیح نمی باشد ");
                        }
                    }
                    */
                }
                catch
                {
                }
                return false;
            }
        }
    }
}
