using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace ConsoleAppKavi
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //string outputFilePath = "E:\\EG\\EG Files\\POSLOG\\All Output Files -Kavi\\";
                //string xmlstring = "F:\\Woolworths-TCS Project\\IM POS AEX Document\\Input Document\\02. NCR R10 POS\\RetailTransactionLog_4700_001_262.xml";
                //var files = Directory.GetFiles("E:\\EG\\EG Files\\POSLOG\\All Input Files -kavi\\All Input Files", "RetailTransactionLog_4700_001_262.xml", SearchOption.AllDirectories);
                string outputFilePath = @"E:\EG\EG Files\POSLOG\Processing_Issue_Files\Output\DCLR_Output";
                //string xmlstring = @"E:\EG\EG Files\POSLOG\Processing_Issue_Files\Input\DCLR FILES\TenderDeclaration_4770.xml";
                //var files = Directory.GetFiles(@"E:\EG\EG Files\POSLOG\Processing_Issue_Files\Input\DCLR FILES", "TenderDeclaration_4770.xml", SearchOption.AllDirectories);

                string xmlstring = @"E:\EG\EG Files\POSLOG\DCLR files\DCLR issue\DCLR Input\schema2\Tender Control T 93559.xml";
                var files = Directory.GetFiles(@"E:\EG\EG Files\POSLOG\DCLR files\DCLR issue\DCLR Input\schema2", "Tender Control T 93559.xml", SearchOption.AllDirectories);
                foreach (string file in files)
                {

                    //if (!File.Exists("F:\\Woolworths-TCS Project\\IM POS AEX Document\\Input Document\\All Input Files\\" + Path.GetFileName(file)))
                    //{
                    //    File.Copy(file, "F:\\Woolworths-TCS Project\\IM POS AEX Document\\Input Document\\All Input Files\\"+Path.GetFileName(file));
                    //}
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlstring = file;
                    // xmlstring = "F:\\Woolworths-TCS Project\\IM POS AEX Document\\Input Document\\02. NCR R10 POS\\Declaration.xml";
                    //  xmlstring = "F:\\Woolworths-TCS Project\\IM POS AEX Document\\Input Document\\All Input Files\\PaidOut_4770_-1_30092019_6.xml";
                    xmlDocument.Load(xmlstring);
                    Console.WriteLine(xmlstring);
                    string jsonText = JsonConvert.SerializeXmlNode(xmlDocument, Newtonsoft.Json.Formatting.Indented, false);
                    PosLogRoot objPosLogRoot = JsonConvert.DeserializeObject<PosLogRoot>(jsonText);
                    if (objPosLogRoot != null && objPosLogRoot.POSLog != null)
                    {
                        string csvData = ConvertCSVData(objPosLogRoot);
                        using (StreamWriter streamWriter = new StreamWriter(outputFilePath + Path.GetFileNameWithoutExtension(xmlstring) + ".csv", false))
                            streamWriter.WriteLine(csvData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public static string ConvertCSVData(PosLogRoot objPosLogRoot)
        {

            StringBuilder sb = new StringBuilder();
            string hdr = GetHDR(objPosLogRoot);
            sb.Append(hdr);
            string poseod = GetPOSEOD(objPosLogRoot);
            sb.Append(poseod);
            string streod = GetSTREOD(objPosLogRoot);
            sb.Append(streod);
            string paid = GetPAID(objPosLogRoot);
            sb.Append(paid);
            string cust = GetCUST(objPosLogRoot);
            sb.Append(cust);
            string hdra = GetHDRA(objPosLogRoot);
            sb.Append(hdra);
            string promos = GetPROMOS(objPosLogRoot);
            sb.Append(promos);
            string loyals = GetLOYALS(objPosLogRoot);
            sb.Append(loyals);
            string tendr = GetTENDR(objPosLogRoot);
            sb.Append(tendr);
            string itml = GetITML(objPosLogRoot);
            sb.Append(itml);
            string itmla = GetITMLA(objPosLogRoot);
            sb.Append(itmla);
            string itmlpm = GetITMLPM(objPosLogRoot);
            sb.Append(itmlpm);
            string itmldr = GetITMLDR(objPosLogRoot);
            sb.Append(itmldr);
            string itmltr = GetITMLTR(objPosLogRoot);
            sb.Append(itmltr);
            string itmltax = GetITMLTAX(objPosLogRoot);
            sb.Append(itmltax);
            string dclr = GetDCLR(objPosLogRoot);
            sb.Append(dclr);


            return sb.ToString();
        }

        #region Common Methods
        public static string GetTransactionStatus(Transaction objTransaction)
        {
            string transaction = string.Empty;
            if (objTransaction.RetailTransaction != null)
            {
                transaction = objTransaction.RetailTransaction.TransactionStatus;
            }
            else if (objTransaction.CustomerOrderTransaction != null)
            {
                transaction = objTransaction.CustomerOrderTransaction.TransactionStatus;
            }

            return transaction;
        }
        public static string GetTXType(Transaction objTransaction)
        {
            string transaction = string.Empty;
            if (objTransaction.RetailTransaction != null)
            {
                transaction = objTransaction.RetailTransaction.R10ExTransactionType;
            }
            else if (objTransaction.TenderControlTransaction != null)
            {
                transaction = (objTransaction.TenderControlTransaction.PaidIn != null && objTransaction.TenderControlTransaction.PaidIn.Count > 0 ? (objTransaction.TenderControlTransaction.PaidIn.FirstOrDefault().Reason != null ? objTransaction.TenderControlTransaction.PaidIn.FirstOrDefault().Reason.Text : "") : "");
            }
            else if (objTransaction.FoodServiceTransaction != null)
            {
                transaction = "NA";
            }
            else if (objTransaction.ForeCourtTransaction != null)
            {
                transaction = objTransaction.ForeCourtTransaction.R10ExType;
            }
            else if (objTransaction.ControlTransaction != null)
            {
                transaction = "NoSale";
            }
            else if (objTransaction.CustomerOrderTransaction != null)
            {
                transaction = objTransaction.CustomerOrderTransaction.R10ExTransactionType;
            }
            else if (objTransaction.InventoryControlTransaction != null)
            {
                transaction = (objTransaction.InventoryControlTransaction.InventoryLoss != null ? objTransaction.InventoryControlTransaction.InventoryLoss.TypeCode : "");
            }
            return transaction;
        }
        public static string GetTransactionType(Transaction objTransaction)
        {
            string transaction = string.Empty;
            if (objTransaction.RetailTransaction != null)
            {
                transaction = "RETAILTRANSACTION";
            }
            else if (objTransaction.TenderControlTransaction != null)
            {
                transaction = "TENDERCONTROLTRANSACTION";
            }
            else if (objTransaction.FoodServiceTransaction != null)
            {
                transaction = "FOODSERVICETRANSACTION";
            }
            else if (objTransaction.ForeCourtTransaction != null)
            {
                transaction = "FORECOURTTRANSACTION";
            }
            else if (objTransaction.ControlTransaction != null)
            {
                transaction = "CONTROLTRANSACTION";
            }
            else if (objTransaction.CustomerOrderTransaction != null)
            {
                transaction = "CUSTOMERORDERTRANSACTION";
            }
            else if (objTransaction.InventoryControlTransaction != null)
            {
                transaction = "INVENTORYCONTROLTRANSACTION";
            }
            return transaction;
        }
        public static string GetSaleType(LineItem objLineItem)
        {
            string saleType = string.Empty;

            if (objLineItem.Sale != null)
            {
                saleType = "Sale";
            }
            else if (objLineItem.Sale != null)
            {
                saleType = "Sale";
            }
            else if (objLineItem.Return != null)
            {
                saleType = "Return";
            }
            else if (objLineItem.FuelSale != null)
            {
                saleType = "FuelSale";
            }

            return saleType;

        }

        #endregion

        #region POS Qualifier

        public static string GetHDR(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {

                csvData = "HDR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text
                         + "|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Name
                         + "|" + (objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.SalesOrganization ?? "")
                         + "|" + objPosLogRoot.POSLog.Transaction.WorkstationID.Text
                         + "|" + objPosLogRoot.POSLog.Transaction.WorkstationID.WorkstationLocation
                         + "|" + objPosLogRoot.POSLog.Transaction.WorkstationID.TypeCode
                         + "|" + objPosLogRoot.POSLog.Transaction.SequenceNumber
                         + "|" + objPosLogRoot.POSLog.Transaction.TransactionID
                         + "|" + objPosLogRoot.POSLog.Transaction.BusinessDayDate
                         + "|" + (objPosLogRoot.POSLog.Transaction.BeginDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.BeginDateTime.Text) : "")
                         + "|" + (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "");

                if (objPosLogRoot.POSLog.Transaction.OperatorID != null)
                {
                    csvData += "|" + objPosLogRoot.POSLog.Transaction.OperatorID.Text
                              + "|" + objPosLogRoot.POSLog.Transaction.OperatorID.OperatorName
                              + "|" + objPosLogRoot.POSLog.Transaction.OperatorID.WorkerID
                              + "|" + objPosLogRoot.POSLog.Transaction.OperatorID.OperatorType;
                }
                else
                {
                    csvData += "||||";
                }
                csvData += "|" + objPosLogRoot.POSLog.Transaction.TrainingModeFlag;
                csvData += "|" + GetTransactionType(objPosLogRoot.POSLog.Transaction)
                           + "|" + GetTXType(objPosLogRoot.POSLog.Transaction)
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.r10ExTenderExchange != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.r10ExTenderExchange.Id : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.r10ExTenderExchange != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.r10ExTenderExchange.Name : "")
                           + "|" + GetTransactionStatus(objPosLogRoot.POSLog.Transaction)
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.R10ExReturnedFlag : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.R10ExReturnType : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.InventoryLoss != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.InventoryLoss.Reason : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.ReasonCode : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.ReasonCode : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.BusinessUnit : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink != null ? (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.WorkstationID != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.WorkstationID.Text : "") : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.SequenceNumber : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.InventoryControlTransaction != null && objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.InventoryControlTransaction.TransactionLink.BusinessDayDate.Date : "")

                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApproverID != null ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApproverID.Text : "") : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApproverID != null ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApproverID.OperatorName : "") : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApproverID != null ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApproverID.OperatorType : "") : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().BusinessRuleManager.BusinessRule.RuleName : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().BusinessRuleManager.BusinessRule.BusinessAction.MessageName : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().BusinessRuleManager.BusinessRule.BusinessAction.ActionType : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().BusinessRuleManager.BusinessRule.BusinessAction.IsApproved : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.Count > 0 ? (objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApprovalDateTime != null ? objPosLogRoot.POSLog.Transaction.OperatorBypassApproval.FirstOrDefault().ApprovalDateTime.Text : "") : "")


                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Count > 0 ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.FirstOrDefault().CurrencyCode : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Count > 0 ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.FirstOrDefault().Text : "")

                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionNetAmount").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionNetAmount").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTotalVoidAmount").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTotalVoidAmount").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTaxIncluded").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTaxIncluded").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTaxSurcharge").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTaxSurcharge").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTaxExemptAmount").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTaxExemptAmount").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionGrandAmount").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionGrandAmount").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTenderApplied").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTenderApplied").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTotalSavings").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTotalSavings").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "CashbackTotalAmount").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "CashbackTotalAmount").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTotalReturnAmount").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionTotalReturnAmount").FirstOrDefault().Text : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionPurchaseQuantity").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Total.Where(x => x.TotalType == "TransactionPurchaseQuantity").FirstOrDefault().Text : "")

                           + "|" + (objPosLogRoot.POSLog.Transaction.FoodServiceTransaction != null && objPosLogRoot.POSLog.Transaction.FoodServiceTransaction.OrderTime != null ? objPosLogRoot.POSLog.Transaction.FoodServiceTransaction.OrderTime.SaleStartType : "")

                           + "|" + (objPosLogRoot.POSLog.Transaction != null && objPosLogRoot.POSLog.Transaction.R10ExRetailerId != null ? objPosLogRoot.POSLog.Transaction.R10ExRetailerId : "")
                           + "|" + (objPosLogRoot.POSLog.Transaction != null && objPosLogRoot.POSLog.Transaction.R10ExSettlementDate != null ? objPosLogRoot.POSLog.Transaction.R10ExSettlementDate.Text : "")
                           + "|||||";

                sb.Append(csvData);
                sb.AppendLine();
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("HDR Error : " + ex.Message);
                csvData = ex.Message;
            }
            return csvData;
        }
        public static string GetPOSEOD(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.ControlTransaction != null)
            {
                if (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderSummary != null)
                {
                    foreach (var objTenderSummary in objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderSummary)
                    {
                        csvData = "POSEOD|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                objPosLogRoot.POSLog.Transaction.BusinessDayDate
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.TillID != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.TillID : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.OpenBusinessDayDate != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.OpenBusinessDayDate : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.CloseBusinessDayDate != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.CloseBusinessDayDate : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TransactionCount : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalNetSalesAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalNetSalesAmount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalNetSalesAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalNetSalesAmount.Currency : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalNetReturnAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalNetReturnAmount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalTaxAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TotalTaxAmount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.GrossPositiveAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.GrossPositiveAmount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans.Total != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans.Total.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans.Total.Amount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans.Total != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans.Total.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Loans.Total.Amount.Currency : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup.Totals != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup.Totals.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup.Totals.Amount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup.Totals != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup.Totals.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.TenderPickup.Totals.Amount.Currency : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount.Currency : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidIn != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidIn.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidIn != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidIn.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidOut != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidOut.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount.Text : "") : "")
                               + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD != null ? (objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidOut != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.PaidOut.Amount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.BusinessEOD.SessionSettle.Refunds.Amount.Text : "") : "")
                               + "|" + (objTenderSummary != null && objTenderSummary.Sales != null ? objTenderSummary.Sales.TenderID : "")
                               + "|" + (objTenderSummary != null && objTenderSummary.Sales != null ? objTenderSummary.Sales.Count : "")
                               + "|" + (objTenderSummary != null && objTenderSummary.Sales != null ? objTenderSummary.Sales.R10ExTenderDescription : "")
                               + "|" + (objTenderSummary != null && objTenderSummary.Sales != null ? objTenderSummary.Sales.TenderType : "")
                               + "|" + (objTenderSummary != null && objTenderSummary.Sales != null && objTenderSummary.Sales.Amount != null ? objTenderSummary.Sales.Amount.Text : "")
                               + "|" + (objTenderSummary != null && objTenderSummary.Sales != null && objTenderSummary.Sales.Amount != null ? objTenderSummary.Sales.Amount.Currency : "")
                               ;

                        sb.Append(csvData);
                        sb.AppendLine();
                    }
                }

            }

            return sb.ToString();
        }
        public static string GetSTREOD(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.ControlTransaction != null)
            {
                if (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal != null)
                {

                    csvData = "STREOD|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                                            objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                                            objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                                            objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                                            (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                                            objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExFinanceDate : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExTransactionCount : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExGrossSales != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExGrossSales.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExGrossSales != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExGrossSales.Currency : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExGrossSales != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExRetailSummary.R10ExGST : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTransactionCount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTransactionCount : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExGrossAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExGrossAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExGrossAmount != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExGrossAmount.Currency : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary != null && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExGST != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExGST : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Tender Loan").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Tender Loan").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Tender Pickup").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Tender Pickup").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Safe Transfer").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Safe Transfer").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Bank Deposit").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Bank Deposit").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Bank Receipt").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Bank Receipt").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Paid In").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Paid In").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Paid Out").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Paid Out").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Auto Tender Pickup").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Auto Tender Pickup").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Auto Reconcile Tender").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Auto Reconcile Tender").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Pos Transfer").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Pos Transfer").FirstOrDefault().R10ExAmount.Text : "")
                                                           + "|" + (objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Count > 0 && objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Declarations").FirstOrDefault() != null ? objPosLogRoot.POSLog.Transaction.ControlTransaction.R10ExStoreEOD.R10ExCashOfficeSummary.R10ExTotal.Where(x => x.Type == "Declarations").FirstOrDefault().R10ExAmount.Text : "")
                                                           ;

                    sb.Append(csvData);
                    sb.AppendLine();


                }
            }

            return sb.ToString();

        }
        public static string GetPAID(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction != null)
            {
                if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.PaidIn != null)
                {
                    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.PaidIn)
                    {
                        csvData = "PAID|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                 + "|" + (obj != null ? obj.TenderID : "")
                                 + "|" + (obj != null ? "PaidIn" : "")
                                 + "|" + (obj != null && obj.Amount != null ? obj.Amount.Text : "")
                                 + "|" + (obj != null && obj.Reason != null ? obj.Reason.Text : "")
                                 + "|" + (obj != null && obj.Amount != null ? obj.Amount.Currency : "")
                                 + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Source.Text : "")
                                 + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Destination != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Destination.Text : "")

                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxExclusiveAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxExclusiveAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxInclusiveAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxInclusiveAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount.Text : "")
                               ;

                        sb.Append(csvData);
                        sb.AppendLine();
                    }
                }

                if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.PaidOut != null)
                {
                    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.PaidOut)
                    {
                        csvData = "PAID|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                  + "|" + (obj != null ? obj.TenderID : "")
                                 + "|" + (obj != null ? "PaidIn" : "")
                                 + "|" + (obj != null && obj.Amount != null ? obj.Amount.Text : "")
                                 + "|" + (obj != null && obj.Reason != null ? obj.Reason.Text : "")
                                 + "|" + (obj != null && obj.Amount != null ? obj.Amount.Currency : "")
                                 + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Source.Text : "")
                                 + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Destination != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.Account.Destination.Text : "")

                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxExclusiveAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxExclusiveAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxInclusiveAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotalTaxInclusiveAmount.Text : "")
                                   + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.R10ExTotals.R10ExTotal.R10ExAmount.Text : "")
                               ;

                        sb.Append(csvData);
                        sb.AppendLine();
                    }
                }
            }

            return sb.ToString();
        }
        public static string GetITML(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    csvData = "ITML|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                     objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                     objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                     objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                     (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                     objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                     obj.SequenceNumber
                                     + "|" + (obj.Sale != null ? obj.Sale.ItemLink : "")
                                     + "|" + (obj.BeginDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.BeginDateTime.Text) : "")
                                     + "|" + (obj.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.EndDateTime.Text) : "")
                                     + "|" + GetSaleType(obj)
                                     + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.R10ExReturnedFlag : "")
                                     + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.R10ExReturnType : "")
                                     + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.TransactionStatus : "")
                                     + "|" + (obj.Sale != null ? obj.Sale.NotNormallyStockedFlag : "")
                                     + "|" + (obj != null ? obj.EntryMethod : "")
                                     + "|" + (obj != null ? "" : "")
                                     + "|" + (obj != null ? "" : "")
                                     + "|" + (obj.ScanData != null ? obj.ScanData.Text : "")
                                     + "|" + (obj.ScanData != null ? obj.ScanData.TypeCode : "")
                                     + "|" + (obj.ScanData != null ? obj.ScanData.R10ExSubType : "")
                                     + "|" + (obj.Sale != null ? obj.Sale.ItemType : (obj.Return != null ? obj.Return.ItemType : ""))
                                     + "|" + (obj.Sale != null ? (obj.Sale.POSIdentity != null ? obj.Sale.POSIdentity.POSIDType : "") : (obj.Return != null ? (obj.Return.POSIdentity != null ? obj.Return.POSIdentity.POSIDType : "") : ""))
                                     + "|" + (obj.Sale != null ? (obj.Sale.POSIdentity != null ? obj.Sale.POSIdentity.POSItemID : "") : (obj.Return != null ? (obj.Return.POSIdentity != null ? obj.Return.POSIdentity.POSItemID : "") : ""))
                                     + "|" + (obj.Sale != null ? (obj.Sale.ItemID != null ? obj.Sale.ItemID.Text : "") : (obj.Return != null ? (obj.Return.ItemID != null ? obj.Return.ItemID.Text : "") : ""))
                                     + "|" + (obj.Sale != null ? (obj.Sale.ItemID != null ? obj.Sale.ItemID.Type : "") : (obj.Return != null ? (obj.Return.ItemID != null ? obj.Return.ItemID.Type : "") : ""))
                                     + "|" + (obj != null ? obj.VoidFlag : "")
                                     + "|" + (obj != null ? (obj.Coupon != null ? obj.Coupon.Amount : "") : "")
                                     + "|" + (obj != null ? obj.RefusalReason : "")
                                     + "|" + (obj != null ? obj.Action : "")
                                     + "|" + (obj.Sale != null ? (obj.Sale.MerchandiseHierarchy != null && obj.Sale.MerchandiseHierarchy.Count > 0 ? obj.Sale.MerchandiseHierarchy.FirstOrDefault().Text : "") : (obj.Return != null ? (obj.Return.MerchandiseHierarchy != null && obj.Return.MerchandiseHierarchy.Count > 0 ? obj.Return.MerchandiseHierarchy.FirstOrDefault().Text : "") : ""))
                                     + "|" + (obj.Sale != null ? (obj.Sale.MerchandiseHierarchy != null && obj.Sale.MerchandiseHierarchy.Count > 0 ? obj.Sale.MerchandiseHierarchy.FirstOrDefault().ID : "") : (obj.Return != null ? (obj.Return.MerchandiseHierarchy != null && obj.Return.MerchandiseHierarchy.Count > 0 ? obj.Return.MerchandiseHierarchy.FirstOrDefault().ID : "") : ""))
                                     + "|" + (obj != null ? (obj.Sale != null && obj.Sale.Description != null && obj.Sale.Description.Count > 0 ? obj.Sale.Description.FirstOrDefault().Text : "") : "")
                                     + "|" + (obj.FuelSale != null ? (obj.FuelSale.RegularSalesUnitPrice != null ? obj.FuelSale.RegularSalesUnitPrice.Currency : "") : "")
                                     + "|" + (obj.FuelSale != null ? (obj.FuelSale.RegularSalesUnitPrice != null ? obj.FuelSale.RegularSalesUnitPrice.Text : "") : "")
                                     + "|" + (obj.Sale != null ? (obj.Sale.ActualSalesUnitPrice != null ? obj.Sale.ActualSalesUnitPrice.Text : "") : "")
                                     + "|" + (obj.Sale != null ? (obj.Sale.ExtendedAmount != null ? obj.Sale.ExtendedAmount.Text : "") : "")
                                     + "|" + (obj.FuelSale != null ? (obj.FuelSale.ExtendedDiscountAmount != null ? obj.FuelSale.ExtendedDiscountAmount.Text : "") : "")
                                     + "|" + (obj.FuelSale != null ? (obj.FuelSale.Quantity != null ? obj.FuelSale.Quantity.Text : "") : "")
                                     + "|" + ((obj.Sale != null && obj.Sale.Quantity != null) ? obj.Sale.Quantity.Units : (obj.FuelSale != null && obj.FuelSale.Quantity != null ? obj.FuelSale.Quantity.Units : ""))
                                     + "|" + ((obj.Sale != null && obj.Sale.Quantity != null) ? obj.Sale.Quantity.UnitOfMeasureCode : (obj.FuelSale != null && obj.FuelSale.Quantity != null ? obj.FuelSale.Quantity.UnitOfMeasureCode : ""))
                                     + "|" + (obj.Return != null ? (obj.Return.ReturnPolicy != null && obj.Return.ReturnPolicy.ReturnPolicyId != null ? obj.Return.ReturnPolicy.ReturnPolicyId.Text : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.Disposal != null ? obj.Return.Disposal.Method : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.Reason != null ? obj.Return.Reason : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? obj.Return.TransactionLink.BusinessUnit : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? (obj.Return.TransactionLink.WorkstationID != null ? obj.Return.TransactionLink.WorkstationID.Text : "") : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? (obj.Return.TransactionLink.BusinessDayDate != null ? obj.Return.TransactionLink.BusinessDayDate.Date : "") : "") : "")
                                     + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? (objPosLogRoot.POSLog.Transaction.RetailTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.TransactionLink.TransactionID : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? obj.Return.TransactionLink.SequenceNumber : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? obj.Return.TransactionLink.LineItemSequenceNumber : "") : "")
                                     + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction != null ? (objPosLogRoot.POSLog.Transaction.RetailTransaction.TransactionLink != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.TransactionLink.ReasonCode : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.Return.TransactionLink.BeginDateTime) : "") : "")
                                     + "|" + (obj.Return != null ? (obj.Return.TransactionLink != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.Return.TransactionLink.EndDateTime) : "") : "")
                                     + "|" + ((obj.FuelSale != null && obj.FuelSale.FuelingPointID != null) ? obj.FuelSale.FuelingPointID : "")
                                     + "|" + ((obj.FuelSale != null && obj.FuelSale.NozzleID != null) ? obj.FuelSale.NozzleID : "")
                                     + "|" + ((obj.FuelSale != null && obj.FuelSale.TankID != null) ? obj.FuelSale.TankID.Text : "")
                                     + "|" + ((obj.FuelSale != null && obj.FuelSale.TankID != null) ? obj.FuelSale.TankID.BlendRatio : "");

                    sb.Append(csvData);
                    sb.AppendLine();
                }
            }


            return sb.ToString();

        }
        public static string GetTENDR(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    // if (obj.Tender.TenderType == "")
                    //{
                    csvData = "TENDR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                     objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                     objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                     objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                     (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                     objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                     obj.SequenceNumber
                                     + "|" + (obj.BeginDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.BeginDateTime.Text) : "")
                                     + "|" + (obj.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.EndDateTime.Text) : "")
                                     + "|" + (obj != null ? obj.VoidFlag : "")
                                     + "|" + (obj != null ? obj.EntryMethod : "")
                                     + "|" + (obj != null ? obj.RefusalTypeCode : "")
                                     + "|" + (obj.Tender != null ? obj.Tender.TenderType : "")
                                     + "|" + (obj.Tender != null ? obj.Tender.TypeCode : "")
                                     + "|" + (obj.Tender != null ? obj.Tender.R10ExTenderDescription : "")
                                     + "|" + (obj.Tender != null ? obj.Tender.R10ExPromotionId : "")
                                     + "|" + (obj.Tender != null ? obj.Tender.Amount.Text : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Amount != null ? obj.Tender.Amount.Currency : "")
                                     + "|" + (obj.Tender != null ? obj.Tender.Cashback.Text : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Cashback != null ? obj.Tender.Cashback.Currency : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.SignatureRequiredFlag : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.VerifiedByPINFlag : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.RequestedAmount.Text : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null && obj.Tender.Authorization.RequestedAmount != null ? obj.Tender.Authorization.RequestedAmount.Currency : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.AuthorizationCode : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.ReferenceNumber : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.MerchantNumber : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.ProviderID : "")

                                     + "|" + ((obj.Tender != null && obj.Tender.Authorization != null && obj.Tender.Authorization.AuthorizationDateTime != null) ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", obj.Tender.Authorization.AuthorizationDateTime.Text) : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.AuthorizingTermID : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null && obj.Tender.Authorization.R10ExProviderData != null ? obj.Tender.Authorization.R10ExProviderData.R10ExAuthorizationResponseText : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.R10ExCardId : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.R10ExCardType : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Authorization != null ? obj.Tender.Authorization.R10ExAuthorizationType : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Rounding != null ? obj.Tender.Rounding.Text : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Rounding != null ? obj.Tender.Rounding.Currency : "")
                                     + "|" + (obj.Tender != null && obj.Tender.Rounding != null ? obj.Tender.Rounding.RoundingDirection : "")
                                     + "|" + (obj.Tender != null && obj.Tender.TenderChange != null && obj.Tender.TenderChange.Amount != null ? obj.Tender.TenderChange.Amount.Text : "")
                                     + "|" + (obj.Tender != null && obj.Tender.TenderChange != null && obj.Tender.TenderChange.Amount != null ? obj.Tender.TenderChange.Amount.Currency : "")
                                   ;

                    sb.Append(csvData);
                    sb.AppendLine();
                    //}
                }
            }


            return sb.ToString();

        }
        public static string GetPROMOS(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.PromotionsSummary != null)
            {
                foreach (var objPromos in objPosLogRoot.POSLog.Transaction.RetailTransaction.PromotionsSummary.PromotionSummary)
                {
                    csvData = "PROMOS|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                     objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                     objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                     objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                     (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                     objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                    + "|" + (objPromos != null ? objPromos.PromotionID : "")
                                     + "|" + (objPromos != null ? objPromos.RedemptionQuantity : "")
                                    + "|" + (objPromos != null ? objPromos.TotalRewardAmount.Text : "")
                                    + "|" + (objPromos != null && objPromos.TotalRewardAmount != null ? objPromos.TotalRewardAmount.Type : "")
                                    + "|" + (objPromos != null && objPromos.TotalRewardAmount != null ? objPromos.TotalRewardAmount.Currency : "")
                                    + "|" + (objPromos != null ? objPromos.RewardType : "")
                                    + "|" + (objPromos != null && objPromos.IssuedCoupons != null && objPromos.IssuedCoupons.IssuesCoupon != null ? objPromos.IssuedCoupons.IssuesCoupon.ScanCode : "")
                                    + "|" + (objPromos != null && objPromos.IssuedCoupons != null && objPromos.IssuedCoupons.IssuesCoupon != null ? objPromos.IssuedCoupons.IssuesCoupon.StartDate : "")
                                    + "|" + (objPromos != null && objPromos.IssuedCoupons != null && objPromos.IssuedCoupons.IssuesCoupon != null ? objPromos.IssuedCoupons.IssuesCoupon.ExpiryDate : "")
                                    + "|" + (objPromos != null ? objPromos.PromotionDescription : "")
                                    + "|" + (objPromos != null ? objPromos.QualifyingSpent : "")
                                    + "|" + (objPromos != null ? objPromos.TriggerTiming : "")
                                    + "|" + (objPromos != null && objPromos.LoyaltyAccount != null && objPromos.LoyaltyAccount.LoyaltyProgram != null ? objPromos.LoyaltyAccount.LoyaltyProgram.FirstOrDefault().LoyaltyAccountID.Text : "")
                                    + "|" + (objPromos != null && objPromos.LoyaltyAccount != null && objPromos.LoyaltyAccount.LoyaltyProgram != null && objPromos.LoyaltyAccount.LoyaltyProgram.FirstOrDefault().Points != null ? objPromos.LoyaltyAccount.LoyaltyProgram.FirstOrDefault().Points.FirstOrDefault().Text : "")
                                    + "|" + (objPromos != null && objPromos.LoyaltyAccount != null && objPromos.LoyaltyAccount.LoyaltyProgram != null && objPromos.LoyaltyAccount.LoyaltyProgram.FirstOrDefault().Points != null ? objPromos.LoyaltyAccount.LoyaltyProgram.FirstOrDefault().Points.FirstOrDefault().Type : "")
                                    + "|" + (objPromos != null ? objPromos.Message : "")
                                    + "|" + (objPromos != null ? objPromos.LoyaltyContributionSplit : "")
                                    ;

                    sb.Append(csvData);
                    sb.AppendLine();
                }

            }


            return sb.ToString();

        }
        public static string GetITMLPM(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    if (obj.FuelSale != null && obj.FuelSale.RetailPriceModifier != null)
                    {

                        foreach (var objfuel in obj.FuelSale.RetailPriceModifier)
                        {
                            csvData = "ITMLPM|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objfuel != null ? (objfuel.SequenceNumber != null ? objfuel.SequenceNumber : "") : "")
                                   + "|" + (objfuel != null ? (objfuel.Description != null ? objfuel.Description : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null ? objfuel.MethodCode : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null ? objfuel.ReasonCode : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Text : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Currency : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Action : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.PreviousPrice != null ? objfuel.PreviousPrice.Text : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null ? objfuel.PromotionID : "") : "")

                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().ParticipatorId : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")

                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.RewardSplitAmount != null ? objfuel.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.RewardSplitAmount != null ? objfuel.LineItemRewardPromotion.RewardSplitAmount.Currency : "") : "")

                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.Units : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.UnitOfMeasureCode : "") : "")
                                   + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null ? objfuel.LineItemRewardPromotion.TriggerAmount : "") : "")

                                   ;

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else
                    {
                        if (obj.Sale != null && obj.Sale.RetailPriceModifier != null)
                        {
                            foreach (var objfuel in obj.Sale.RetailPriceModifier)
                            {
                                csvData = "ITMLPM|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                        objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                        objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                        objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                        (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                        objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                        obj.SequenceNumber
                                       + "|" + (objfuel != null ? (objfuel.SequenceNumber != null ? objfuel.SequenceNumber : "") : "")
                                       + "|" + (objfuel != null ? (objfuel.Description != null ? objfuel.Description : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null ? objfuel.MethodCode : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null ? objfuel.ReasonCode : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Currency : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Action : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.PreviousPrice != null ? objfuel.PreviousPrice.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null ? objfuel.PromotionID : "") : "")

                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().ParticipatorId : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")

                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.RewardSplitAmount != null ? objfuel.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.RewardSplitAmount != null ? objfuel.LineItemRewardPromotion.RewardSplitAmount.Currency : "") : "")

                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.Units : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.UnitOfMeasureCode : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null ? objfuel.LineItemRewardPromotion.TriggerAmount : "") : "")

                                       ;

                                sb.Append(csvData);
                                sb.AppendLine();
                            }

                        }

                        if ((obj.Return != null && obj.Return.RetailPriceModifier != null))
                        {
                            foreach (var objfuel in obj.Return.RetailPriceModifier)
                            {
                                csvData = "ITMLPM|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                        objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                        objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                        objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                        (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                        objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                        obj.SequenceNumber
                                       + "|" + (objfuel != null ? (objfuel.SequenceNumber != null ? objfuel.SequenceNumber : "") : "")
                                       + "|" + (objfuel != null ? (objfuel.Description != null ? objfuel.Description : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null ? objfuel.MethodCode : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null ? objfuel.ReasonCode : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Currency : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.Amount != null ? objfuel.Amount.Action : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.PreviousPrice != null ? objfuel.PreviousPrice.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null ? objfuel.PromotionID : "") : "")

                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().ParticipatorId : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.ApportionmentAmount != null ? objfuel.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")

                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.RewardSplitAmount != null ? objfuel.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.RewardSplitAmount != null ? objfuel.LineItemRewardPromotion.RewardSplitAmount.Currency : "") : "")

                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.Units : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null && objfuel.LineItemRewardPromotion.TriggerQuantity != null ? objfuel.LineItemRewardPromotion.TriggerQuantity.UnitOfMeasureCode : "") : "")
                                       + "|" + (objfuel != null ? (objfuel != null && objfuel.LineItemRewardPromotion != null ? objfuel.LineItemRewardPromotion.TriggerAmount : "") : "")
                                       ;

                                sb.Append(csvData);
                                sb.AppendLine();
                            }

                        }

                    }


                }
            }

            return sb.ToString();

        }
        public static string GetLOYALS(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.LoyaltyAccount != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.LoyaltyAccount.LoyaltyProgram != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LoyaltyAccount.LoyaltyProgram)
                {

                    csvData = "LOYALS|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                                            objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                                            objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                                            objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                                            (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                                            objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                                           + "|" + (obj.LoyaltyAccountID != null ? obj.LoyaltyAccountID.Text : "")
                                                           + "|" + (obj.LoyaltyAccountID != null ? obj.LoyaltyAccountID.Description : "")
                                                           + "|" + (obj.Points != null && obj.Points.Count > 0 && obj.Points.Where(x => x.Type == "X:OpenBalance").FirstOrDefault() != null ? obj.Points.Where(x => x.Type == "X:OpenBalance").FirstOrDefault().Text : "")
                                                           + "|" + (obj.Points != null && obj.Points.Count > 0 && obj.Points.Where(x => x.Type == "Balance").FirstOrDefault() != null ? obj.Points.Where(x => x.Type == "Balance").FirstOrDefault().Text : "")
                                                           + "|" + (obj.Points != null && obj.Points.Count > 0 && obj.Points.Where(x => x.Type == "Redeemed").FirstOrDefault() != null ? obj.Points.Where(x => x.Type == "Redeemed").FirstOrDefault().Text : "")
                                                           + "|" + (obj.Points != null && obj.Points.Count > 0 && obj.Points.Where(x => x.Type == "PointsEarned").FirstOrDefault() != null ? obj.Points.Where(x => x.Type == "PointsEarned").FirstOrDefault().Text : "")

                                                           ;

                    sb.Append(csvData);
                    sb.AppendLine();

                }
            }

            return sb.ToString();

        }
        public static string GetITMLTAX(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    if (obj.FuelSale != null && obj.FuelSale.Tax != null)
                    {

                        foreach (var objtax in obj.FuelSale.Tax)
                        {
                            csvData = "ITMLTAX|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objtax != null ? (objtax.SequenceNumber != null ? objtax.SequenceNumber : "") : "")
                                   + "|" + (objtax != null ? (objtax.TypeCode != null ? objtax.TypeCode : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExAmountBeforeRounding != null ? Math.Round(Convert.ToDecimal(objtax.R10ExAmountBeforeRounding), 2) : "") : "")
                                    + "|" + (objtax != null ? (objtax.R10ExTaxableAmountBeforeRounding != null ? Math.Round(Convert.ToDecimal(objtax.R10ExTaxableAmountBeforeRounding), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxAuthority != null ? objtax.TaxAuthority : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.TaxIncludedInTaxableAmountFlag : "") : "")
                                   + "|" + (objtax != null ? (objtax.Amount != null ? objtax.Amount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.Amount != null ? objtax.Amount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.Percent != null ? Math.Round(Convert.ToDecimal(objtax.Percent), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxRuleID != null ? objtax.TaxRuleID : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxGroupID != null ? objtax.TaxGroupID : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExProratedDiscount != null ? objtax.R10ExProratedDiscount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExProratedDiscount != null ? objtax.R10ExProratedDiscount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExSellPrice != null ? objtax.R10ExSellPrice.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExSellPrice != null ? objtax.R10ExSellPrice.Currency : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else if (obj.Sale != null && obj.Sale.Tax != null)
                    {
                        foreach (var objtax in obj.Sale.Tax)
                        {
                            csvData = "ITMLTAX|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objtax != null ? (objtax.SequenceNumber != null ? objtax.SequenceNumber : "") : "")
                                   + "|" + (objtax != null ? (objtax.TypeCode != null ? objtax.TypeCode : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExAmountBeforeRounding != null ? Math.Round(Convert.ToDecimal(objtax.R10ExAmountBeforeRounding), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExTaxableAmountBeforeRounding != null ? Math.Round(Convert.ToDecimal(objtax.R10ExTaxableAmountBeforeRounding), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxAuthority != null ? objtax.TaxAuthority : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.TaxIncludedInTaxableAmountFlag : "") : "")
                                   + "|" + (objtax != null ? (objtax.Amount != null ? objtax.Amount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.Amount != null ? objtax.Amount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.Percent != null ? Math.Round(Convert.ToDecimal(objtax.Percent), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxRuleID != null ? objtax.TaxRuleID : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxGroupID != null ? objtax.TaxGroupID : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExProratedDiscount != null ? objtax.R10ExProratedDiscount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExProratedDiscount != null ? objtax.R10ExProratedDiscount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExSellPrice != null ? objtax.R10ExSellPrice.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExSellPrice != null ? objtax.R10ExSellPrice.Currency : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else if (obj.Return != null && obj.Return.Tax != null)
                    {
                        foreach (var objtax in obj.Return.Tax)
                        {
                            csvData = "ITMLTAX|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objtax != null ? (objtax.SequenceNumber != null ? objtax.SequenceNumber : "") : "")
                                   + "|" + (objtax != null ? (objtax.TypeCode != null ? objtax.TypeCode : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExAmountBeforeRounding != null ? Math.Round(Convert.ToDecimal(objtax.R10ExAmountBeforeRounding), 2) : "") : "")
                                  + "|" + (objtax != null ? (objtax.R10ExTaxableAmountBeforeRounding != null ? Math.Round(Convert.ToDecimal(objtax.R10ExTaxableAmountBeforeRounding), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxAuthority != null ? objtax.TaxAuthority : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxableAmount != null ? objtax.TaxableAmount.TaxIncludedInTaxableAmountFlag : "") : "")
                                   + "|" + (objtax != null ? (objtax.Amount != null ? objtax.Amount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.Amount != null ? objtax.Amount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.Percent != null ? Math.Round(Convert.ToDecimal(objtax.Percent), 2) : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxRuleID != null ? objtax.TaxRuleID : "") : "")
                                   + "|" + (objtax != null ? (objtax.TaxGroupID != null ? objtax.TaxGroupID : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExProratedDiscount != null ? objtax.R10ExProratedDiscount.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExProratedDiscount != null ? objtax.R10ExProratedDiscount.Currency : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExSellPrice != null ? objtax.R10ExSellPrice.Text : "") : "")
                                   + "|" + (objtax != null ? (objtax.R10ExSellPrice != null ? objtax.R10ExSellPrice.Currency : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }


                }
            }

            return sb.ToString();

        }
        public static string GetCUST(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null)
            {


                List<Name> lst = new List<Name>();
                if (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Name != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Name.Name != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Name.Name.Count > 0)
                {


                    foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Name.Name)
                    {
                        if (obj.GetType().Name == "String")
                        {
                            lst.Add(new Name() { Text = Convert.ToString(obj) });
                        }
                        else if (obj.GetType().Name == "JObject")
                        {
                            lst.Add(JsonConvert.DeserializeObject<Name>(Convert.ToString(obj)));
                        }
                    }

                }

                csvData = "CUST|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                                        objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                                        objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                                        objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                                        (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                                        objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.CustomerID != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.CustomerID : "")
                                                       + "|" + (lst != null && lst.Count > 0 && lst.Where(x => x.TypeCode == null).FirstOrDefault() != null ? lst.Where(x => x.TypeCode == null).FirstOrDefault().Text : "")
                                                       + "|" + (lst != null && lst.Count > 0 && lst.Where(x => x.TypeCode == "GivenName").FirstOrDefault() != null ? lst.Where(x => x.TypeCode == "GivenName").FirstOrDefault().Text : "")
                                                       + "|" + (lst != null && lst.Count > 0 && lst.Where(x => x.TypeCode == "FamilyName").FirstOrDefault() != null ? lst.Where(x => x.TypeCode == "FamilyName").FirstOrDefault().Text : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.EMail != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.EMail.EMailAddress : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.TelephoneNumber != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.TelephoneNumber.LocalNumber : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExScanData : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExCustomerExternalId : "")

                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExCustomerType : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExLoyaltyCardScannedTime : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExEntryMethod : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExIsAuthenticatedOffline : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExIsAnonymous : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.R10ExRetroactiveProcessActivate : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle.LicensePlateID : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle.Model : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle.Color : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle.AdditionalInfo : "")
                                                       + "|" + (objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle != null ? objPosLogRoot.POSLog.Transaction.RetailTransaction.Customer.Vehicle.UniqueLoyaltyTransactionID : "")
                                                       ;

                sb.Append(csvData);
                sb.AppendLine();
            }

            return sb.ToString();

        }
        public static string GetITMLDR(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    if (obj.FuelSale != null && obj.FuelSale.Tax != null && obj.FuelSale.PromotionDeferredRewards != null)
                    {

                        foreach (var objpromotiondeferredrewards in obj.FuelSale.PromotionDeferredRewards)
                        {
                            csvData = "ITMLDR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.SequenceNumber != null ? objpromotiondeferredrewards.SequenceNumber : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.PromotionID != null ? objpromotiondeferredrewards.PromotionID : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.DeferredID != null ? objpromotiondeferredrewards.DeferredID : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Description != null ? objpromotiondeferredrewards.Description : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Type != null ? objpromotiondeferredrewards.Type : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Value != null ? objpromotiondeferredrewards.Value : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiondeferredrewards.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null ? objpromotiondeferredrewards.LineItemRewardPromotion.TriggerAmount : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null ? objpromotiondeferredrewards.LineItemRewardPromotion.RewardLevel : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else if (obj.Sale != null && obj.Sale.PromotionDeferredRewards != null)
                    {
                        foreach (var objpromotiondeferredrewards in obj.Sale.PromotionDeferredRewards)
                        {
                            csvData = "ITMLDR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.SequenceNumber != null ? objpromotiondeferredrewards.SequenceNumber : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.PromotionID != null ? objpromotiondeferredrewards.PromotionID : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.DeferredID != null ? objpromotiondeferredrewards.DeferredID : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Description != null ? objpromotiondeferredrewards.Description : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Type != null ? objpromotiondeferredrewards.Type : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Value != null ? objpromotiondeferredrewards.Value : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiondeferredrewards.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null ? objpromotiondeferredrewards.LineItemRewardPromotion.TriggerAmount : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null ? objpromotiondeferredrewards.LineItemRewardPromotion.RewardLevel : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else if (obj.Return != null && obj.Return.PromotionDeferredRewards != null)
                    {
                        foreach (var objpromotiondeferredrewards in obj.Return.PromotionDeferredRewards)
                        {
                            csvData = "ITMLDR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.SequenceNumber != null ? objpromotiondeferredrewards.SequenceNumber : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.PromotionID != null ? objpromotiondeferredrewards.PromotionID : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.DeferredID != null ? objpromotiondeferredrewards.DeferredID : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Description != null ? objpromotiondeferredrewards.Description : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Type != null ? objpromotiondeferredrewards.Type : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.Value != null ? objpromotiondeferredrewards.Value : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiondeferredrewards.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null ? objpromotiondeferredrewards.LineItemRewardPromotion.TriggerAmount : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null && objpromotiondeferredrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiondeferredrewards.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                   + "|" + (objpromotiondeferredrewards != null ? (objpromotiondeferredrewards.LineItemRewardPromotion != null ? objpromotiondeferredrewards.LineItemRewardPromotion.RewardLevel : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }


                }
            }

            return sb.ToString();

        }
        public static string GetITMLTR(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    if (obj.FuelSale != null && obj.FuelSale.Tax != null && obj.FuelSale.PromotionTenderRewards != null)
                    {

                        foreach (var objpromotiontenderrewards in obj.FuelSale.PromotionTenderRewards)
                        {
                            csvData = "ITMLTR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                    objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                    objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                    (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                    objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                    obj.SequenceNumber
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.SequenceNumber != null ? objpromotiontenderrewards.SequenceNumber : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Type != null ? objpromotiontenderrewards.Type : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Value != null ? objpromotiontenderrewards.Value : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.PromotionID != null ? objpromotiontenderrewards.PromotionID : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Description != null ? objpromotiontenderrewards.Description : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.Units : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.UnitOfMeasureCode : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.EntryMethod : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount.Currency : "") : "")
                                   + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardLevel : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else if (obj.Sale != null && obj.Sale.PromotionTenderRewards != null)
                    {
                        foreach (var objpromotiontenderrewards in obj.Sale.PromotionTenderRewards)
                        {
                            csvData = "ITMLTR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                   objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                   objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                   objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                   (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                   objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                   obj.SequenceNumber
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.SequenceNumber != null ? objpromotiontenderrewards.SequenceNumber : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Type != null ? objpromotiontenderrewards.Type : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Value != null ? objpromotiontenderrewards.Value : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.PromotionID != null ? objpromotiontenderrewards.PromotionID : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Description != null ? objpromotiontenderrewards.Description : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.Units : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.UnitOfMeasureCode : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.EntryMethod : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount.Currency : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardLevel : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }
                    else if (obj.Return != null && obj.Return.PromotionTenderRewards != null)
                    {
                        foreach (var objpromotiontenderrewards in obj.Return.PromotionTenderRewards)
                        {
                            csvData = "ITMLTR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                   objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                   objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                   objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                   (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                   objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                   obj.SequenceNumber
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.SequenceNumber != null ? objpromotiontenderrewards.SequenceNumber : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Type != null ? objpromotiontenderrewards.Type : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Value != null ? objpromotiontenderrewards.Value : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.PromotionID != null ? objpromotiontenderrewards.PromotionID : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.Description != null ? objpromotiontenderrewards.Description : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.Text : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.Units : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.UnitOfMeasureCode : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity != null ? objpromotiontenderrewards.LineItemRewardPromotion.TriggerQuantity.EntryMethod : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Text : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Currency : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.ApportionmentAmount.FirstOrDefault().Participator : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount.Text : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null && objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardSplitAmount.Currency : "") : "")
                                  + "|" + (objpromotiontenderrewards != null ? (objpromotiontenderrewards.LineItemRewardPromotion != null ? objpromotiontenderrewards.LineItemRewardPromotion.RewardLevel : "") : "");

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }


                }
            }

            return sb.ToString();

        }
        public static string GetHDRA(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction != null && objPosLogRoot.POSLog.Transaction.OperatorBypassApproval != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.OperatorBypassApproval)
                {
                    csvData = "HDRA|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                                            objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                                            objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                                            objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                                            (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "")
                                                            + "|" + objPosLogRoot.POSLog.Transaction.BusinessDayDate
                                                            + "|" + (obj != null ? obj.SequenceNumber : "")
                                                            + "|" + (obj != null && obj.ApproverID != null ? obj.ApproverID.Text : "")
                                                            + "|" + (obj != null && obj.ApproverID != null ? obj.ApproverID.OperatorName : "")
                                                            + "|" + (obj != null && obj.ApproverID != null ? obj.ApproverID.OperatorType : "")
                                                            + "|" + (obj != null ? obj.Description : "")
                                                            + "|" + (obj != null && obj.BusinessRuleManager != null && obj.BusinessRuleManager.BusinessRule != null ? obj.BusinessRuleManager.BusinessRule.RuleName : "")

                                                            + "|" + (obj != null && obj.BusinessRuleManager != null && obj.BusinessRuleManager.BusinessRule != null && obj.BusinessRuleManager.BusinessRule.BusinessAction != null ? obj.BusinessRuleManager.BusinessRule.BusinessAction.MessageName : "")
                                                            + "|" + (obj != null && obj.BusinessRuleManager != null && obj.BusinessRuleManager.BusinessRule != null && obj.BusinessRuleManager.BusinessRule.BusinessAction != null ? obj.BusinessRuleManager.BusinessRule.BusinessAction.ActionType : "")
                                                            + "|" + (obj != null && obj.BusinessRuleManager != null && obj.BusinessRuleManager.BusinessRule != null && obj.BusinessRuleManager.BusinessRule.BusinessAction != null ? obj.BusinessRuleManager.BusinessRule.BusinessAction.IsApproved : "")
                                                           ;

                    sb.Append(csvData);
                    sb.AppendLine();
                }
            }

            return sb.ToString();

        }
        public static string GetITMLA(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.RetailTransaction != null && objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.RetailTransaction.LineItem)
                {
                    if (obj.OperatorBypassApproval != null)
                        foreach (var objOperatorBypassApproval in obj.OperatorBypassApproval)
                        {
                            csvData = "ITMLA|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text + "|" +
                                        objPosLogRoot.POSLog.Transaction.WorkstationID.Text + "|" +
                                        objPosLogRoot.POSLog.Transaction.SequenceNumber + "|" +
                                        objPosLogRoot.POSLog.Transaction.TransactionID + "|" +
                                        (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "") + "|" +
                                        objPosLogRoot.POSLog.Transaction.BusinessDayDate + "|" +
                                        obj.SequenceNumber
                                        + "|" + objOperatorBypassApproval.SequenceNumber
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.ApproverID != null ? objOperatorBypassApproval.ApproverID.Text : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.ApproverID != null ? objOperatorBypassApproval.ApproverID.OperatorType : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.ApproverID != null ? objOperatorBypassApproval.ApproverID.OperatorName : "")

                                       + "|" + (objOperatorBypassApproval != null ? objOperatorBypassApproval.Description : "")
                                       //  + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.RuleName : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction.MessageName : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction.ActionType : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction.IsApproved : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction.ReasonGroup : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction.ReasonName : "")
                                       + "|" + (objOperatorBypassApproval != null && objOperatorBypassApproval.BusinessRuleManager != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule != null && objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction != null ? objOperatorBypassApproval.BusinessRuleManager.BusinessRule.BusinessAction.ReasonCode : "")
                                   ;

                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                }
            }

            return sb.ToString();

        }
        public static string GetDCLR(PosLogRoot objPosLogRoot)
        {
            string csvData = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.SafeSettle != null)
            {
                foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.SafeSettle)
                {

                    //boomer.
                    csvData = "DCLR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text
                        + "|" + objPosLogRoot.POSLog.Transaction.WorkstationID.Text
                        + "|" + objPosLogRoot.POSLog.Transaction.SequenceNumber
                       + "|" + objPosLogRoot.POSLog.Transaction.TransactionID
                       + "|" + (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "")
                       + "|" + objPosLogRoot.POSLog.Transaction.BusinessDayDate
                        + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort != null ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().TenderID :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().TenderID : "")) : "")

                             + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort != null ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().TenderID :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().TenderID : "")) : "")

                             + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort != null ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().TenderType :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().TenderType : "")) : "")

                             + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort != null ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().Amount.Text != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().Amount.Text :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().Amount.Text : "")) : "")

                             + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort != null ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over.FirstOrDefault().Amount.Currency :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short.FirstOrDefault().Amount.Currency : "")) : "")

                             + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration != null ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source.Text :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination.Text : "")) : "")

                             + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source.ExternalId :
                            (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination != null) ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination.ExternalId : "")

                            + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending.Amount.Text : "")
                            + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total.Amount.Text : "")
                            + "|" + (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals.Amount.Text : "")

                            + "|" + (obj.TenderOpenBalanceTotalAmount != null ? obj.TenderOpenBalanceTotalAmount.Text : "")
                            + "|" + (obj.TenderOpenBalanceTotalAmount != null ? obj.TenderOpenBalanceTotalAmount.R10ExOverShort : "")
                            + "|" + (obj.TenderCloseBalanceTotalAmount != null ? obj.TenderCloseBalanceTotalAmount.Text : "")
                            + "|" + (obj.TenderCloseBalanceTotalAmount != null ? obj.TenderCloseBalanceTotalAmount.R10ExOverShort : "")
                            + "|" + (obj.TenderLoanTotalAmount != null ? obj.TenderLoanTotalAmount.Text : "")
                            + "|" + (obj.TotalTenderLoanCount != null ? obj.TotalTenderLoanCount : "")
                            + "|" + (obj.TenderPickupTotalAmount != null ? obj.TenderPickupTotalAmount.Text : "")
                            + "|" + (obj.TotalTenderPickupCount != null ? obj.TotalTenderPickupCount : "")
                            + "|" + (obj.TenderDepositTotalAmount != null ? obj.TenderDepositTotalAmount.Text : "")
                            + "|" + (obj.TenderReceiptTotalAmount != null ? obj.TenderReceiptTotalAmount.Text : "")
                          ;

                    sb.Append(csvData);
                    sb.AppendLine();

                }
            }
            else if(objPosLogRoot.POSLog.Transaction.TenderControlTransaction!= null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration!=null 
                && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort != null)
            {
                String UnitID = objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text;
                String WorkstationId = objPosLogRoot.POSLog.Transaction.WorkstationID.Text;
                String POSTXSequenceNumber = objPosLogRoot.POSLog.Transaction.SequenceNumber;
                String POSTXID = objPosLogRoot.POSLog.Transaction.TransactionID;
                String POSTXEndDateTime = objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "";
                String BusinessDayDate = objPosLogRoot.POSLog.Transaction.BusinessDayDate;
                String TillID = "";
                String TillExternalId="";

                //Following Fields to be firld based on types of records Totals/Overshorts.
                String TenderID = "";
                String DeclarationType = "";
                String TenderType = "";
                String Amount = "";
                String Currency = "";

                //currently Not ingested in Overshort scenario: source Production Table query
                String TenderSummaryAmount = "";
                String LoansAmount = "";
                String TenderPickupAmount = "";
                String TenderOpenBalanceTotalAmount = "";
                String TenderOpenBalanceOverShort = "";
                String TenderCloseBalanceTotalAmount = "";
                String TenderCloseBalanceOverShort = "";
                String TenderLoanTotalAmount = "";
                String TotalTenderLoanCount = "";
                String TenderPickupTotalAmount = "";
                String TotalTenderPickupCount = "";
                String TenderDepositTotalAmount = "";
                String TenderReceiptTotalAmount = "";

                if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account1 != null) {
                    TillID = objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account1.Text;
                    TillExternalId = objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account1.ExternalId;
                }

                //For Single Totals File case.
                if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1 != null)
                {
                    TenderID = objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.TenderID != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.TenderID : "";
                    DeclarationType = "";
                    Amount= (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount.Text != null) ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount.Text : "";
                    Currency = (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount.Currency != null) ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount.Currency : "";

                    csvData = "DCLR|" + UnitID
                       + "|" + WorkstationId
                       + "|" + POSTXSequenceNumber
                       + "|" + POSTXID
                       + "|" + POSTXEndDateTime
                       + "|" + BusinessDayDate
                       + "|" + TenderID
                       + "|" + DeclarationType
                       + "|" + TenderType
                       + "|" + Amount
                       + "|" + Currency
                       + "|" + TillID
                       + "|" + TillExternalId
                       + "|" + TenderSummaryAmount
                       + "|" + LoansAmount
                       + "|" + TenderPickupAmount
                       + "|" + TenderOpenBalanceTotalAmount
                       + "|" + TenderOpenBalanceOverShort
                       + "|" + TenderCloseBalanceTotalAmount
                       + "|" + TenderCloseBalanceOverShort
                       + "|" + TenderLoanTotalAmount
                       + "|" + TotalTenderLoanCount
                       + "|" + TenderPickupTotalAmount
                       + "|" + TotalTenderPickupCount
                       + "|" + TenderDepositTotalAmount
                       + "|" + TenderReceiptTotalAmount;
                    sb.Append(csvData);
                    sb.AppendLine();
                }
                if(objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Totals != null){
                    //Case: for multiple totals. To ingest only rows with 
                    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Totals) {
                        Amount = obj.Amount.Text != null ? obj.Amount.Text : "";
                        Boolean isAmountValid = (Amount.Equals("0") || Amount.Equals("0.0")) ? false : true;
                        if (isAmountValid) {
                            TenderID = obj.TenderID!=null? obj.TenderID:"";
                            DeclarationType = "";
                            TenderType = "";
                            Currency = obj.Amount.Currency != null ? obj.Amount.Currency : "";
                            csvData = "DCLR|" + UnitID
                      + "|" + WorkstationId
                      + "|" + POSTXSequenceNumber
                      + "|" + POSTXID
                      + "|" + POSTXEndDateTime
                      + "|" + BusinessDayDate
                      + "|" + TenderID
                      + "|" + DeclarationType
                      + "|" + TenderType
                      + "|" + Amount
                      + "|" + Currency
                      + "|" + TillID
                      + "|" + TillExternalId
                      + "|" + TenderSummaryAmount
                      + "|" + LoansAmount
                      + "|" + TenderPickupAmount
                      + "|" + TenderOpenBalanceTotalAmount
                      + "|" + TenderOpenBalanceOverShort
                      + "|" + TenderCloseBalanceTotalAmount
                      + "|" + TenderCloseBalanceOverShort
                      + "|" + TenderLoanTotalAmount
                      + "|" + TotalTenderLoanCount
                      + "|" + TenderPickupTotalAmount
                      + "|" + TotalTenderPickupCount
                      + "|" + TenderDepositTotalAmount
                      + "|" + TenderReceiptTotalAmount;
                            sb.Append(csvData);
                            sb.AppendLine();
                        }

                    }

                }

                if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null)
                    //Extracting Overshort Over- Objects
                {
                    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over) {
                        TenderID = obj.TenderID.text!=null? obj.TenderID.text:"";
                        DeclarationType = "Over";
                        TenderType = obj.TenderType != null ? obj.TenderType:"";
                        Amount = (obj.Amount!=null && obj.Amount.Text!=null)?obj.Amount.Text:"";
                        Currency = (obj.Amount!=null && obj.Amount.Currency!=null)?obj.Amount.Currency:"";
                        csvData = "DCLR|" + UnitID
                       + "|" + WorkstationId
                       + "|" + POSTXSequenceNumber
                       + "|" + POSTXID
                       + "|" + POSTXEndDateTime
                       + "|" + BusinessDayDate
                       + "|" + TenderID
                       + "|" + DeclarationType
                       + "|" + TenderType
                       + "|" + Amount
                       + "|" + Currency
                       + "|" + TillID
                       + "|" + TillExternalId
                       + "|" + TenderSummaryAmount
                       + "|" + LoansAmount
                       + "|" + TenderPickupAmount
                       + "|" + TenderOpenBalanceTotalAmount
                       + "|" + TenderOpenBalanceOverShort
                       + "|" + TenderCloseBalanceTotalAmount
                       + "|" + TenderCloseBalanceOverShort
                       + "|" + TenderLoanTotalAmount
                       + "|" + TotalTenderLoanCount
                       + "|" + TenderPickupTotalAmount
                       + "|" + TotalTenderPickupCount
                       + "|" + TenderDepositTotalAmount
                       + "|" + TenderReceiptTotalAmount;
                        sb.Append(csvData);
                        sb.AppendLine();
                    }
                        
                }
                if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null)
                {
                    //If list contans short elements
                    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short)
                    {
                        TenderID = obj.TenderID.text != null ? obj.TenderID.text : "";
                        DeclarationType = "Short";
                        TenderType = obj.TenderType != null ? obj.TenderType : "";
                        Amount = (obj.Amount != null && obj.Amount.Text != null) ? obj.Amount.Text : "";
                        Currency = (obj.Amount != null && obj.Amount.Currency != null) ? obj.Amount.Currency : "";
                        csvData = "DCLR|" + UnitID
                       + "|" + WorkstationId
                       + "|" + POSTXSequenceNumber
                       + "|" + POSTXID
                       + "|" + POSTXEndDateTime
                       + "|" + BusinessDayDate
                       + "|" + TenderID
                       + "|" + DeclarationType
                       + "|" + TenderType
                       + "|" + Amount
                       + "|" + Currency
                       + "|" + TillID
                       + "|" + TillExternalId
                       + "|" + TenderSummaryAmount
                       + "|" + LoansAmount
                       + "|" + TenderPickupAmount
                       + "|" + TenderOpenBalanceTotalAmount
                       + "|" + TenderOpenBalanceOverShort
                       + "|" + TenderCloseBalanceTotalAmount
                       + "|" + TenderCloseBalanceOverShort
                       + "|" + TenderLoanTotalAmount
                       + "|" + TotalTenderLoanCount
                       + "|" + TenderPickupTotalAmount
                       + "|" + TotalTenderPickupCount
                       + "|" + TenderDepositTotalAmount
                       + "|" + TenderReceiptTotalAmount;
                        sb.Append(csvData);
                        sb.AppendLine();
                    }
   
                }

                //schema1 case scenario
                //if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over != null)
                //{

                //    //If list contans over elements
                //    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Over)
                //    {
                //        Boolean TenderDeclarationFlag = false;
                //        Boolean TillSettleFlag = false;
                //        Boolean Total1Flag = false;
                //        if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration != null)
                //        {
                //            TenderDeclarationFlag = true;
                //        }
                //        if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle != null)
                //        {
                //            TillSettleFlag = true;
                //        }
                //        if (TenderDeclarationFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1 != null)
                //        {
                //            Total1Flag = true;
                //        }
                //        csvData = "DCLR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text
                //                    + "|" + objPosLogRoot.POSLog.Transaction.WorkstationID.Text
                //                    + "|" + objPosLogRoot.POSLog.Transaction.SequenceNumber
                //                    + "|" + objPosLogRoot.POSLog.Transaction.TransactionID
                //                    + "|" + (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "")
                //                    + "|" + objPosLogRoot.POSLog.Transaction.BusinessDayDate
                //                    + "|" + (obj != null ? obj.TenderID.text : "")
                //                    + "|" + (obj != null ? obj.TenderType : "")
                //                    + "|" + (obj != null && obj.Amount.Text != null ? obj.Amount.Text : "")
                //                    + "|" + (obj != null && obj.Amount != null ? obj.Amount.Currency : "")
                //                    + "|" + (TenderDeclarationFlag ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source.Text : (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination.Text : "")) : "")
                //                    + "|" + (TenderDeclarationFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source.ExternalId : (TenderDeclarationFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination != null) ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination.ExternalId : "")

                //                                    + "|" + (TillSettleFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending.Amount.Text : "")
                //                                    + "|" + (TillSettleFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total.Amount.Text : "")
                //                                    + "|" + (TillSettleFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals.Amount.Text : (
                //                                    Total1Flag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount.Text : ""
                //                                    ))
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                    + "|" + ("")
                //                                  ;

                //        sb.Append(csvData);
                //        sb.AppendLine();

                //    }
                //}
                //if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short != null)
                //{
                //    //If list contans over elements
                //    foreach (var obj in objPosLogRoot.POSLog.Transaction.TenderControlTransaction.OverShort.Short)
                //    {
              
                //            Boolean TenderDeclarationFlag = false;
                //            Boolean TillSettleFlag = false;
                //            Boolean Total1Flag = false;
                //            if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration != null)
                //            {
                //                TenderDeclarationFlag = true;
                //            }
                //            if (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle != null)
                //            {
                //                TillSettleFlag = true;
                //            }
                //            if (TenderDeclarationFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1 != null)
                //            {
                //                Total1Flag = true;
                //            }
                //            csvData = "DCLR|" + objPosLogRoot.POSLog.Transaction.BusinessUnit.UnitID.Text
                //                        + "|" + objPosLogRoot.POSLog.Transaction.WorkstationID.Text
                //                        + "|" + objPosLogRoot.POSLog.Transaction.SequenceNumber
                //                        + "|" + objPosLogRoot.POSLog.Transaction.TransactionID
                //                        + "|" + (objPosLogRoot.POSLog.Transaction.EndDateTime != null ? string.Format("{0:yyyy-MM-dd'T'HH:mm:ss.fff}", objPosLogRoot.POSLog.Transaction.EndDateTime.Text) : "")
                //                        + "|" + objPosLogRoot.POSLog.Transaction.BusinessDayDate
                //                        + "|" + (obj != null ? obj.TenderID.text : "")
                //                        + "|" + (obj != null ? obj.TenderType : "")
                //                        + "|" + (obj != null && obj.Amount.Text != null ? obj.Amount.Text : "")
                //                        + "|" + (obj != null && obj.Amount != null ? obj.Amount.Currency : "")
                //                        + "|" + (TenderDeclarationFlag ? (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source.Text : (objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination.Text : "")) : "")
                //                        + "|" + (TenderDeclarationFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Source.ExternalId : (TenderDeclarationFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination != null) ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Account.Destination.ExternalId : "")

                //                                        + "|" + (TillSettleFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderSummary.Ending.Amount.Text : "")
                //                                        + "|" + (TillSettleFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.Loans.Total.Amount.Text : "")
                //                                        + "|" + (TillSettleFlag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals != null && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TillSettle.TenderPickup.Totals.Amount.Text : (
                //                                        Total1Flag && objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount != null ? objPosLogRoot.POSLog.Transaction.TenderControlTransaction.TenderDeclaration.Total1.Amount.Text : ""
                //                                        ))
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                        + "|" + ("")
                //                                      ;

                //            sb.Append(csvData);
                //            sb.AppendLine();

                //        }
                //    }

            }

            return sb.ToString();

        }

        #endregion
    }
}
