using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppKavi
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class UnitID
    {
        [JsonProperty("@Name")]
        public string Name { get; set; }

        [JsonProperty("@SalesOrganization")]
        public string SalesOrganization { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class BusinessUnit
    {
        [JsonConverter(typeof(SingleValueArrayConverter<UnitID>))]
        public UnitID UnitID { get; set; }
    }

    public class WorkstationID
    {
        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("@WorkstationLocation")]
        public string WorkstationLocation { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class OperatorID
    {
        [JsonProperty("@OperatorName")]
        public string OperatorName { get; set; }

        [JsonProperty("@WorkerID")]
        public string WorkerID { get; set; }

        [JsonProperty("@OperatorType")]
        public string OperatorType { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ReceiptImage
    {
        [JsonProperty("@r10Ex:ReceiptKind")]
        public string R10ExReceiptKind { get; set; }

        [JsonProperty("@r10Ex:ReceiptFormat")]
        public string R10ExReceiptFormat { get; set; }

        [JsonProperty("@r10Ex:ReceiptNotPrinted")]
        public string R10ExReceiptNotPrinted { get; set; }
        public string ReceiptLine { get; set; }
    }

    public class POSIdentity
    {
        public string POSItemID { get; set; }
        [JsonProperty("@POSIDType")]
        public string POSIDType { get; set; }
    }

    public class MerchandiseHierarchy
    {
        [JsonProperty("@ID")]
        public string ID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Description
    {
        [JsonProperty("@r10Ex:Culture")]
        public string R10ExCulture { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("@Culture")]
        public string Culture { get; set; }
    }

    public class RegularSalesUnitPrice
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ActualSalesUnitPrice
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ExtendedAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Quantity
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("@EntryMethod")]
        public string EntryMethod { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExConsumableGroup
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string Type { get; set; }
        public string ID { get; set; }
    }

    public class ExtendedDiscountAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Amount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("@Action")]
        public string Action { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TriggerQuantity
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("@EntryMethod")]
        public string EntryMethod { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ApportionmentAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("@Participator")]
        public string Participator { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@ParticipatorId")]
        public string ParticipatorId { get; set; }
    }

    public class RewardSplitAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class RewardSplitUnitAmount
    {
        [JsonProperty("@SequenceNumber")]
        public string SequenceNumber { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class UnitLines
    {
        public RewardSplitUnitAmount RewardSplitUnitAmount { get; set; }
    }

    public class LineItemRewardPromotion
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }

        [JsonProperty("TriggerQuantity")]
        [JsonConverter(typeof(SingleValueArrayConverter<TriggerQuantity>))]
        public TriggerQuantity TriggerQuantity { get; set; }
        public string TriggerAmount { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<ApportionmentAmount>))]
        public List<ApportionmentAmount> ApportionmentAmount { get; set; }
        public RewardSplitAmount RewardSplitAmount { get; set; }
        public UnitLines UnitLines { get; set; }
        public string RewardLevel { get; set; }
    }

    public class RetailPriceModifier
    {
        public string SequenceNumber { get; set; }
        public Amount Amount { get; set; }
        public string PromotionID { get; set; }
        public string Description { get; set; }
        public LineItemRewardPromotion LineItemRewardPromotion { get; set; }

        [JsonProperty("@MethodCode")]
        public string MethodCode { get; set; }
        public PreviousPrice PreviousPrice { get; set; }
        public string ReasonCode { get; set; }
    }
    public class PreviousPrice
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }
    public class TaxableAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("@TaxIncludedInTaxableAmountFlag")]
        public string TaxIncludedInTaxableAmountFlag { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TaxPerUnits
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public object TaxPerUnit { get; set; }

    }

    public class R10ExProratedDiscount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExSellPrice
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Tax
    {
        [JsonProperty("@r10Ex:Sign")]
        public string R10ExSign { get; set; }

        [JsonProperty("@r10Ex:Imposition")]
        public string R10ExImposition { get; set; }

        [JsonProperty("@r10Ex:AmountBeforeRounding")]
        public string R10ExAmountBeforeRounding { get; set; }

        [JsonProperty("@r10Ex:TaxableAmountBeforeRounding")]
        public string R10ExTaxableAmountBeforeRounding { get; set; }
        public string SequenceNumber { get; set; }
        public string TaxAuthority { get; set; }
        public TaxableAmount TaxableAmount { get; set; }
        public Amount Amount { get; set; }
        public string Percent { get; set; }
        public string TaxRuleID { get; set; }
        public string TaxGroupID { get; set; }
        public TaxPerUnits TaxPerUnits { get; set; }

        [JsonProperty("r10Ex:ProratedDiscount")]
        public R10ExProratedDiscount R10ExProratedDiscount { get; set; }

        [JsonProperty("r10Ex:SellPrice")]
        public R10ExSellPrice R10ExSellPrice { get; set; }

        [JsonProperty("r10Ex:TaxLiable")]
        public R10ExTaxLiable R10ExTaxLiable { get; set; }

        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }
    }

    public class OriginalQuantity
    {
        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@EntryMethod")]
        public string EntryMethod { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class GroupIndicator
    {
        [JsonProperty("@Indicator")]
        public string Indicator { get; set; }

        [JsonProperty("@Group")]
        public string Group { get; set; }

        [JsonProperty("@Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("@Priority")]
        public string Priority { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
    }

    public class PromotionDeferredRewards
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string PromotionID { get; set; }
        public string Description { get; set; }
        public LineItemRewardPromotion LineItemRewardPromotion { get; set; }
        public string SequenceNumber { get; set; }
        public string DeferredID { get; set; }
    }

    public class Sale
    {
        public POSIdentity POSIdentity { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<MerchandiseHierarchy>))]
        public List<MerchandiseHierarchy> MerchandiseHierarchy { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<ItemID>))]
        public ItemID ItemID { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Description>))]
        public List<Description> Description { get; set; }
        public RegularSalesUnitPrice RegularSalesUnitPrice { get; set; }
        public ActualSalesUnitPrice ActualSalesUnitPrice { get; set; }
        public ExtendedAmount ExtendedAmount { get; set; }
        public Quantity Quantity { get; set; }

        [JsonProperty("r10Ex:ConsumableGroup")]
        public List<R10ExConsumableGroup> R10ExConsumableGroup { get; set; }
        public string ItemLink { get; set; }
        public ExtendedDiscountAmount ExtendedDiscountAmount { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<RetailPriceModifier>))]
        public List<RetailPriceModifier> RetailPriceModifier { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Tax>))]
        public List<Tax> Tax { get; set; }
        public OriginalQuantity OriginalQuantity { get; set; }
        public GroupIndicator GroupIndicator { get; set; }

        [JsonProperty("PromotionDeferredRewards")]
        [JsonConverter(typeof(SingleValueArrayConverter<PromotionDeferredRewards>))]
        public List<PromotionDeferredRewards> PromotionDeferredRewards { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<PromotionTenderRewards>))]
        public List<PromotionTenderRewards> PromotionTenderRewards { get; set; }

        [JsonProperty("@NotNormallyStockedFlag")]
        public string NotNormallyStockedFlag { get; set; }

        public ConsumableGroup ConsumableGroup { get; set; }

        [JsonProperty("@ItemType")]
        public string ItemType { get; set; }

    }

    public class BusinessAction
    {
        [JsonProperty("@MessageName")]
        public string MessageName { get; set; }

        [JsonProperty("@ActionType")]
        public string ActionType { get; set; }

        [JsonProperty("@IsApproved")]
        public string IsApproved { get; set; }

        [JsonProperty("@ReasonGroup")]
        public string ReasonGroup { get; set; }

        [JsonProperty("@ReasonName")]
        public string ReasonName { get; set; }

        [JsonProperty("@ReasonCode")]
        public string ReasonCode { get; set; }



        [JsonProperty("@FormName")]
        public string FormName { get; set; }
    }

    public class BusinessRule
    {
        [JsonProperty("@RuleName")]
        public string RuleName { get; set; }
        public BusinessAction BusinessAction { get; set; }
    }

    public class BusinessRuleManager
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public BusinessRule BusinessRule { get; set; }
    }

    public class OperatorBypassApproval
    {
        public string SequenceNumber { get; set; }

        public string Description { get; set; }
        public BusinessRuleManager BusinessRuleManager { get; set; }
        public ApproverID ApproverID { get; set; }
        public ApprovalDateTime ApprovalDateTime { get; set; }

    }

    public class Cashback
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Rounding
    {
        [JsonProperty("@RoundingDirection")]
        public string RoundingDirection { get; set; }
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TenderChange
    {
        public Amount Amount { get; set; }
    }

    public class Tender
    {
        [JsonProperty("@TenderType")]
        public string TenderType { get; set; }
        public Amount Amount { get; set; }
        public Cashback Cashback { get; set; }
        public Rounding Rounding { get; set; }
        public string TenderID { get; set; }

        [JsonProperty("r10Ex:TenderDescription")]
        public string R10ExTenderDescription { get; set; }

        [JsonProperty("r10Ex:IsAutoReconcile")]
        public string R10ExIsAutoReconcile { get; set; }

        [JsonProperty("r10Ex:PromotionId")]
        public string R10ExPromotionId { get; set; }
        public Authorization Authorization { get; set; }

        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        public TenderChange TenderChange { get; set; }

    }

    public class R10ExTaxLiable
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Disposal
    {
        [JsonProperty("@Method")]
        public string Method { get; set; }
    }
    public class LineItem
    {
        [JsonProperty("@EntryMethod")]
        public string EntryMethod { get; set; }

        [JsonProperty("@RefusalReason")]
        public string RefusalReason { get; set; }

        [JsonProperty("@RefusalTypeCode")]
        public string RefusalTypeCode { get; set; }

        public Sale Sale { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<ScanData>))]
        public ScanData ScanData { get; set; }
        public string SequenceNumber { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<BeginDateTime>))]
        public BeginDateTime BeginDateTime { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<EndDateTime>))]
        public EndDateTime EndDateTime { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<OperatorBypassApproval>))]
        public List<OperatorBypassApproval> OperatorBypassApproval { get; set; }

        [JsonProperty("@VoidFlag")]
        public string VoidFlag { get; set; }

        [JsonProperty("@Action")]
        public string Action { get; set; }
        public Tender Tender { get; set; }
        public Tax Tax { get; set; }
        public FuelSale FuelSale { get; set; }

        public Return Return { get; set; }
        public Coupon Coupon { get; set; }
    }

    public class Total
    {
        [JsonProperty("@CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }

        [JsonProperty("@TotalType")]
        public string TotalType { get; set; }

        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Amount>))]
        public Amount Amount { get; set; }

        [JsonProperty("TenderID")]
        public string TenderID;
    }

    public class Customer
    {
        [JsonProperty("@r10Ex:IsAuthenticatedOffline")]
        public string R10ExIsAuthenticatedOffline { get; set; }

        [JsonProperty("@r10Ex:IsAnonymous")]
        public string R10ExIsAnonymous { get; set; }

        [JsonProperty("@r10Ex:RetroactiveProcessActivate")]
        public string R10ExRetroactiveProcessActivate { get; set; }
      
        [JsonProperty("Name")]
        public NameClass Name { get; set; }

        [JsonProperty("r10Ex:CustomerType")]
        public string R10ExCustomerType { get; set; }
        public EMail EMail { get; set; }
        public Address Address { get; set; }

        [JsonProperty("r10Ex:ScanData")]
        public string R10ExScanData { get; set; }

        [JsonProperty("r10Ex:CustomerExternalId")]
        public string R10ExCustomerExternalId { get; set; }

        [JsonProperty("r10Ex:LoyaltyCardScannedTime")]
        public string R10ExLoyaltyCardScannedTime { get; set; }
        [JsonProperty("r10Ex:EntryMethod")]
        public string R10ExEntryMethod { get; set; }
        public string CustomerID { get; set; }
        public Vehicle Vehicle { get; set; }
        public TelephoneNumber TelephoneNumber { get; set; }
    }
    public class EMail
    {
        public string EMailAddress { get; set; }
    }

    public class LoyaltyAccountID
    {
        [JsonProperty("@Description")]
        public string Description { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Point
    {
        [JsonProperty("@Type")]
        public string Type { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class LoyaltyProgram
    {

        [JsonConverter(typeof(SingleValueArrayConverter<LoyaltyAccountID>))]
        public LoyaltyAccountID LoyaltyAccountID { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Point>))]
        public List<Point> Points { get; set; }
    }

    public class LoyaltyAccount
    {
        [JsonConverter(typeof(SingleValueArrayConverter<LoyaltyProgram>))]
        public List<LoyaltyProgram> LoyaltyProgram { get; set; }
    }

    public class Descriptions
    {
        [JsonConverter(typeof(SingleValueArrayConverter<Description>))]
        public Description Description { get; set; }
    }

    public class TaxAuthority
    {
        public string AuthorityId { get; set; }
        public Descriptions Descriptions { get; set; }
    }

    public class TaxCalculatedMethodPercent
    {
        public string Value { get; set; }
        public string ImpositionId { get; set; }
    }

    public class TaxRate
    {
        public string RateId { get; set; }
        public string Type { get; set; }
        public string AuthorityId { get; set; }
        public Descriptions Descriptions { get; set; }
        public string IsIncluded { get; set; }
        public TaxCalculatedMethodPercent TaxCalculatedMethodPercent { get; set; }
        public string TaxIndicator { get; set; }
        public string RoundingType { get; set; }
        public string CouponReducesTaxationAmount { get; set; }
    }

    public class TaxDefinitions
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public TaxAuthority TaxAuthority { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<TaxRate>))]
        public List<TaxRate> TaxRate { get; set; }
    }

    public class RefundablePromotions
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string RefundPromotions { get; set; }
    }

    public class R10ExDescription
    {
        [JsonProperty("@r10Ex:Culture")]
        public string R10ExCulture { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExVenueShift
    {
        [JsonProperty("r10Ex:Description")]
        public R10ExDescription R10ExDescription { get; set; }
    }

    public class IssuesCoupon
    {
        [JsonProperty("@IsUnique")]
        public string IsUnique { get; set; }

        [JsonProperty("@DataPatternId")]
        public string DataPatternId { get; set; }

        [JsonProperty("@Identifier")]
        public string Identifier { get; set; }

        [JsonProperty("@StartDate")]
        public string StartDate { get; set; }

        [JsonProperty("@ExpiryDate")]
        public string ExpiryDate { get; set; }
        public string Description { get; set; }
        public string ScanCode { get; set; }
    }

    public class IssuedCoupons
    {
        public IssuesCoupon IssuesCoupon { get; set; }
    }

    public class PromotionSummary
    {
        public string PromotionID { get; set; }
        public string RedemptionQuantity { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<TotalRewardAmount>))]
        public TotalRewardAmount TotalRewardAmount { get; set; }
        public string RewardType { get; set; }
        public string PromotionDescription { get; set; }
        public string QualifyingSpent { get; set; }
        public string Message { get; set; }

        public string LoyaltyContributionSplit { get; set; }
        public string TriggerTiming { get; set; }
        public LoyaltyAccount LoyaltyAccount { get; set; }
        public IssuedCoupons IssuedCoupons { get; set; }
        public RedeemedCoupons RedeemedCoupons { get; set; }
    }
    public class TotalRewardAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("@Type")]
        public string Type { get; set; }


        [JsonProperty("#text")]
        public string Text { get; set; }
    }
    public class PromotionsSummary
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public List<PromotionSummary> PromotionSummary { get; set; }
    }

    public class RetailTransaction
    {
        public List<LineItem> LineItem { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<List<Total>>))]
        public List<Total> Total { get; set; }
        public Customer Customer { get; set; }
        public LoyaltyAccount LoyaltyAccount { get; set; }
        public TaxDefinitions TaxDefinitions { get; set; }
        public RefundablePromotions RefundablePromotions { get; set; }

        [JsonProperty("r10Ex:VenueShift")]
        public R10ExVenueShift R10ExVenueShift { get; set; }

        [JsonProperty("r10Ex:Forms")]
        public object R10ExForms { get; set; }
        public PromotionsSummary PromotionsSummary { get; set; }

        [JsonProperty("r10Ex:IsVoidFuelPromotions")]
        public string R10ExIsVoidFuelPromotions { get; set; }

        [JsonProperty("@r10Ex:TransactionType")]
        public string R10ExTransactionType { get; set; }

        [JsonProperty("@TransactionStatus")]
        public string TransactionStatus { get; set; }

        [JsonProperty("r10Ex:TenderExchange")]
        public r10ExTenderExchange r10ExTenderExchange { get; set; }

        [JsonProperty("@r10Ex:ReturnedFlag")]
        public string R10ExReturnedFlag { get; set; }

        [JsonProperty("@r10Ex:ReturnType")]
        public string R10ExReturnType { get; set; }

        public TransactionLink TransactionLink { get; set; }
    }

    public class r10ExTenderExchange
    {
        [JsonProperty("@Name")]
        public string Name { get; set; }

        [JsonProperty("@Id")]
        public string Id { get; set; }

    }
    public class BeginDateTime
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public DateTime Text { get; set; }
    }

    public class EndDateTime
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public DateTime Text { get; set; }
    }

    public class OrderTime
    {
        [JsonProperty("@SaleStartType")]
        public string SaleStartType { get; set; }
    }

    public class FoodServiceTransaction
    {
        public OrderTime OrderTime { get; set; }
    }

    public class ReturnCustomerDetails
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
    }

    public class Transaction
    {
        [JsonProperty("@MajorVersion")]
        public string MajorVersion { get; set; }

        [JsonProperty("@MinorVersion")]
        public string MinorVersion { get; set; }

        [JsonProperty("@FixVersion")]
        public string FixVersion { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
        public WorkstationID WorkstationID { get; set; }
        public string SequenceNumber { get; set; }
        public string TransactionID { get; set; }
        public OperatorID OperatorID { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<ReceiptImage>))]
        public List<ReceiptImage> ReceiptImage { get; set; }
        public RetailTransaction RetailTransaction { get; set; }
        public string BusinessDayDate { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<BeginDateTime>))]
        public BeginDateTime BeginDateTime { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<BeginDateTime>))]
        public EndDateTime EndDateTime { get; set; }
        public FoodServiceTransaction FoodServiceTransaction { get; set; }

        [JsonProperty("r10Ex:RetailerId")]
        public string R10ExRetailerId { get; set; }

        [JsonProperty("r10Ex:LPEVer")]
        public string R10ExLPEVer { get; set; }
        public ReturnCustomerDetails ReturnCustomerDetails { get; set; }

        public ControlTransaction ControlTransaction { get; set; }

        public ForeCourtTransaction ForeCourtTransaction { get; set; }


        public CustomerOrderTransaction CustomerOrderTransaction { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<OperatorBypassApproval>))]
        public List<OperatorBypassApproval> OperatorBypassApproval { get; set; }
        [JsonProperty("r10Ex:Venue")]
        public string R10ExVenue { get; set; }
        public string TillID { get; set; }

        public TenderControlTransaction TenderControlTransaction { get; set; }
         
        //boomer.
        public List<TenderControlTransaction> TenderControlTransactionList { get; set; }

        [JsonProperty("r10Ex:SettlementDate")]
        public R10ExSettlementDate R10ExSettlementDate { get; set; }

        public InventoryControlTransaction InventoryControlTransaction { get; set; }
        public string TrainingModeFlag { get; set; }


    }

    public class POSLog
    {
        [JsonProperty("@xmlns:r10Ex")]
        public string XmlnsR10Ex { get; set; }

        [JsonProperty("@xmlns:xsi")]
        public string XmlnsXsi { get; set; }

        [JsonProperty("@FixVersion")]
        public string FixVersion { get; set; }

        [JsonProperty("@MajorVersion")]
        public string MajorVersion { get; set; }

        [JsonProperty("@MinorVersion")]
        public string MinorVersion { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public Transaction Transaction { get; set; }
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

    }

    public class PosLogRoot
    {
        [JsonProperty("?xml")]
        public Xml Xml { get; set; }
        public POSLog POSLog { get; set; }

        [JsonProperty("#comment")]
        public List<object> Comment { get; set; }
    }


    public class SingleValueArrayConverter<T> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            object retVal = new Object();
            if (reader.TokenType == JsonToken.PropertyName)
            {
                retVal = null;
                return retVal;
            }
            //else if (reader.Path.Contains("Loans.Total")|| reader.Path.Contains("PaidIn.Amount")|| reader.Path.Contains("TenderPickup.Totals.Amount")|| reader.Path.Contains("SessionSettle.Refunds.Amount"))
            //{
            //    retVal = new Amount() { Text =Convert.ToString(reader.Value) };
            //}
            else if (objectType.Name.Contains("List") && reader.TokenType == JsonToken.PropertyName)
            {
                retVal = null;
                return retVal;
            }
            else if (reader.TokenType == JsonToken.StartObject)
            {
                if (!objectType.Name.Contains("List") && reader.TokenType == JsonToken.StartObject && objectType.IsClass)
                {
                    retVal = serializer.Deserialize(reader, objectType);
                }
                else
                {
                    T instance = (T)serializer.Deserialize(reader, typeof(T));
                    retVal = new List<T>() { instance };
                }
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (objectType.Name == "UnitID")
                {
                    retVal = new UnitID() { Text = reader.Value.ToString() };
                }
                else if (objectType.Name == "ItemID")
                {
                    retVal = new ItemID() { Text = reader.Value.ToString() };
                }
                else if (objectType.Name == "Description")
                {
                    retVal = new Description() { Text = reader.Value.ToString() };
                }
                else if (objectType.Name.Contains("List") && objectType.FullName.Contains("Description"))
                {

                    retVal = new List<Description> { new Description() { Text = reader.Value.ToString() } };

                }
                else if (objectType.Name.Contains("List") && objectType.FullName.Contains("Total"))
                {

                    retVal = new Total() { Text = reader.Value.ToString() };

                }
                else if (objectType.Name == "TriggerQuantity")
                {

                    retVal = new TriggerQuantity() { Text = reader.Value.ToString() };

                }
                else if (objectType.Name == "LoyaltyAccountID")
                {

                    retVal = new LoyaltyAccountID() { Text = reader.Value.ToString() };

                }
                else if (objectType.Name == "Reason")
                {
                    retVal = new Reason() { Text = reader.Value.ToString() };
                }
                else if (objectType.Name == "ScanData")
                {
                    retVal = new ScanData() { Text = reader.Value.ToString() };
                }
                else if (objectType.Name == "TotalRewardAmount")
                {
                    retVal = new TotalRewardAmount() { Text = reader.Value.ToString() };
                }
                else if(reader.Path.Contains("PaidOut")&& objectType.Name == "Amount")
                {
                    retVal = new Amount() { Text = reader.Value.ToString() };
                }
                else if(objectType.Name== "Source")
                {
                    retVal = new Source() { Text = reader.Value.ToString() };
                }
                else if (objectType.Name == "Destination")
                {
                    retVal = new Destination() { Text = reader.Value.ToString() };
                }
                else if (reader.Path.Contains("Inventory") && objectType.Name == "WorkstationID")
                {
                    retVal = new WorkstationID() { Text = reader.Value.ToString() };
                }
                //else if(objectType.Name== "TotalNetSalesAmount")
                //{
                //    retVal = new TotalNetSalesAmount() { Text = reader.Value.ToString() };
                //}
                //else if (objectType.Name == "TotalNetReturnAmount")
                //{
                //    retVal = new TotalNetReturnAmount() { Text = reader.Value.ToString() };
                //}
                //else if (objectType.Name == "TotalTaxAmount")
                //{
                //    retVal = new TotalTaxAmount() { Text = reader.Value.ToString() };
                //}
                //else if (objectType.Name == "GrossPositiveAmount")
                //{
                //    retVal = new GrossPositiveAmount() { Text = reader.Value.ToString() };
                //}
               



            }
            else if (reader.TokenType == JsonToken.Date)
            {
                if (objectType.Name == "BeginDateTime")
                {
                    retVal = new BeginDateTime() { Text = Convert.ToDateTime(reader.Value.ToString()) };

                }
                else if (objectType.Name == "EndDateTime")
                {
                    retVal = new EndDateTime() { Text = Convert.ToDateTime(reader.Value.ToString()) };

                }
                else if (objectType.Name == "StartDateTimestamp")
                {
                    retVal = new StartDateTimestamp() { Text = Convert.ToDateTime(reader.Value.ToString()) };
                }
                else if (objectType.Name == "EndDateTimestamp")
                {
                    retVal = new EndDateTimestamp() { Text = Convert.ToDateTime(reader.Value.ToString()) };
                }
                
            }
            else if ((objectType.FullName.Contains("Total") || objectType.Name == "RetailPriceModifier") && objectType.IsClass)
            {
                if (objectType.FullName.Contains("Total"))
                {
                    retVal = serializer.Deserialize(reader, objectType);
                }
                else
                {
                    retVal = null;
                }
            }
            else if (reader.Path.Contains("Inventory") && objectType.Name == "WorkstationID")
            {
                retVal = new WorkstationID() { Text = reader.Value.ToString() };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                retVal = serializer.Deserialize(reader, objectType);
            }
            else
            {
                retVal = null;
            }
            return retVal;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }


    public class StartDateTimestamp
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public DateTime Text { get; set; }
    }

    public class EndDateTimestamp
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public DateTime Text { get; set; }
    }

    public class TotalNetSalesAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TotalNetReturnAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TotalTaxAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class GrossPositiveAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Sales
    {
        [JsonProperty("@TenderType")]
        public string TenderType { get; set; }
        public Amount Amount { get; set; }
        public string Count { get; set; }
        public string TenderID { get; set; }

        [JsonProperty("r10Ex:TenderDescription")]
        public string R10ExTenderDescription { get; set; }



    }

    public class TenderSummary
    {
        public Ending Ending { get; set; }
        public Sales Sales { get; set; }
    }


    public class Loans
    {
        
        public Total Total { get; set; }
    }

    public class Totals
    {
        [JsonConverter(typeof(SingleValueArrayConverter<TotalNetSalesAmount>))]
        public Amount Amount { get; set; }
    }

    public class TenderPickup
    {
        public Totals Totals { get; set; }
    }

    public class Refunds
    {
        [JsonConverter(typeof(SingleValueArrayConverter<Amount>))]
        public Amount Amount { get; set; }
    }

    public class SessionSettle
    {
        public string TransactionCount { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<TotalNetSalesAmount>))]
        public TotalNetSalesAmount TotalNetSalesAmount { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<TotalNetReturnAmount>))]
        public TotalNetReturnAmount TotalNetReturnAmount { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<TotalTaxAmount>))]
        public TotalTaxAmount TotalTaxAmount { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<GrossPositiveAmount>))]
        public GrossPositiveAmount GrossPositiveAmount { get; set; }
        public List<TenderSummary> TenderSummary { get; set; }
        public Loans Loans { get; set; }
        public TenderPickup TenderPickup { get; set; }
        public Refunds Refunds { get; set; }
        public PaidIn PaidIn { get; set; }
        public PaidOut PaidOut { get; set; }


    }

    public class BusinessEOD
    {
        [JsonConverter(typeof(SingleValueArrayConverter<StartDateTimestamp>))]
        public StartDateTimestamp StartDateTimestamp { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<EndDateTimestamp>))]
        public EndDateTimestamp EndDateTimestamp { get; set; }
        public string TillID { get; set; }
        public string OpenBusinessDayDate { get; set; }
        public string CloseBusinessDayDate { get; set; }
        public SessionSettle SessionSettle { get; set; }



        [JsonProperty("r10Ex:NonResettableGrandTotal")]
        public string R10ExNonResettableGrandTotal { get; set; }

        [JsonProperty("r10Ex:PreviousNonResettableGrandTotal")]
        public string R10ExPreviousNonResettableGrandTotal { get; set; }

        [JsonProperty("r10Ex:PersonalAccounts")]
        public string R10ExPersonalAccounts { get; set; }
    }

    public class ControlTransaction
    {
        public BusinessEOD BusinessEOD { get; set; }
        public string ReasonCode { get; set; }
        public DateTime NoSale { get; set; }
        public BlindPickup BlindPickup { get; set; }

        [JsonProperty("r10Ex:StoreEOD")]
        public R10ExStoreEOD R10ExStoreEOD { get; set; }
    }

    public class ItemID
    {
        [JsonProperty("@Type")]
        public string Type { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }


    public class TankID
    {
        [JsonProperty("@BlendRatio")]
        public string BlendRatio { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }
    public class FuelSale
    {
        public POSIdentity POSIdentity { get; set; }
        public List<MerchandiseHierarchy> MerchandiseHierarchy { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<ItemID>))]
        public ItemID ItemID { get; set; }
        public List<Description> Description { get; set; }
        public RegularSalesUnitPrice RegularSalesUnitPrice { get; set; }
        public ActualSalesUnitPrice ActualSalesUnitPrice { get; set; }
        public ExtendedAmount ExtendedAmount { get; set; }
        public ExtendedDiscountAmount ExtendedDiscountAmount { get; set; }
        public Quantity Quantity { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<RetailPriceModifier>))]
        public List<RetailPriceModifier> RetailPriceModifier { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Tax>))]
        public List<Tax> Tax { get; set; }

        [JsonProperty("r10Ex:ControllerSequenceNumber")]
        public string R10ExControllerSequenceNumber { get; set; }

        [JsonProperty("r10Ex:StartFuelingTime")]
        public DateTime R10ExStartFuelingTime { get; set; }

        [JsonProperty("r10Ex:EndFuelingTime")]
        public DateTime R10ExEndFuelingTime { get; set; }

        [JsonProperty("r10Ex:FlowRate")]
        public string R10ExFlowRate { get; set; }

        [JsonProperty("r10Ex:ConsumableGroup")]
        public List<R10ExConsumableGroup> R10ExConsumableGroup { get; set; }
        public GroupIndicator GroupIndicator { get; set; }

        [JsonProperty("PromotionDeferredRewards")]
        [JsonConverter(typeof(SingleValueArrayConverter<PromotionDeferredRewards>))]
        public List<PromotionDeferredRewards> PromotionDeferredRewards { get; set; }
        public string FuelingPointID { get; set; }
        public string NozzleID { get; set; }
        public TankID TankID { get; set; }

        public List<PromotionTenderRewards> PromotionTenderRewards { get; set; }
    }

    public class ApproverID
    {
        [JsonProperty("@OperatorName")]
        public string OperatorName { get; set; }

        [JsonProperty("@OperatorType")]
        public string OperatorType { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class RequestedAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class AuthorizationDateTime
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public DateTime Text { get; set; }
    }

    public class R10ExProviderData
    {
        [JsonProperty("r10Ex:STAN")]
        public string R10ExSTAN { get; set; }

        [JsonProperty("r10Ex:RRN")]
        public string R10ExRRN { get; set; }

        [JsonProperty("r10Ex:TransactionType")]
        public string R10ExTransactionType { get; set; }

        [JsonProperty("r10Ex:MerchantNumber")]
        public string R10ExMerchantNumber { get; set; }

        [JsonProperty("r10Ex:TerminalID")]
        public string R10ExTerminalID { get; set; }

        [JsonProperty("r10Ex:EPAN")]
        public object R10ExEPAN { get; set; }

        [JsonProperty("r10Ex:TokenPAN")]
        public string R10ExTokenPAN { get; set; }

        [JsonProperty("r10Ex:MaskedPAN")]
        public string R10ExMaskedPAN { get; set; }

        [JsonProperty("r10Ex:AccountType")]
        public string R10ExAccountType { get; set; }

        [JsonProperty("r10Ex:AuthorizationResponseCode")]
        public string R10ExAuthorizationResponseCode { get; set; }

        [JsonProperty("r10Ex:AuthorizationResponseText")]
        public string R10ExAuthorizationResponseText { get; set; }

        [JsonProperty("r10Ex:AuthorizationDate")]
        public string R10ExAuthorizationDate { get; set; }

        [JsonProperty("r10Ex:PAD")]
        public string R10ExPAD { get; set; }
    }
    public class ExtendedData
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
    }
    public class Authorization
    {
        [JsonProperty("@SignatureRequiredFlag")]
        public string SignatureRequiredFlag { get; set; }

        [JsonProperty("@VerifiedByPINFlag")]
        public string VerifiedByPINFlag { get; set; }
        public RequestedAmount RequestedAmount { get; set; }
        public string AuthorizationCode { get; set; }
        public string ReferenceNumber { get; set; }
        public string MerchantNumber { get; set; }
        public string ProviderID { get; set; }
        public AuthorizationDateTime AuthorizationDateTime { get; set; }
        public string AuthorizingTermID { get; set; }
        public string ReceiptText { get; set; }

        [JsonProperty("r10Ex:ProviderData")]
        public R10ExProviderData R10ExProviderData { get; set; }

        [JsonProperty("r10Ex:CardId")]
        public string R10ExCardId { get; set; }

        [JsonProperty("r10Ex:CardType")]
        public string R10ExCardType { get; set; }

        [JsonProperty("r10Ex:AuthorizationType")]
        public string R10ExAuthorizationType { get; set; }
        public ExtendedData ExtendedData { get; set; }
    }

    public class RedeemedCoupon
    {
        [JsonProperty("@SeriesId")]
        public string SeriesId { get; set; }

        [JsonProperty("@OfferId")]
        public string OfferId { get; set; }

        [JsonProperty("@CouponType")]
        public string CouponType { get; set; }
        public Quantity Quantity { get; set; }
        public string ScanCode { get; set; }
    }

    public class RedeemedCoupons
    {
        public RedeemedCoupon RedeemedCoupon { get; set; }
    }

    public class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public class PumpTestVolume
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExMerchandiseHierarchy
    {
        [JsonProperty("@ID")]
        public string ID { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExPOSIdentity
    {
        public string POSItemID { get; set; }
    }

    public class R10ExRegularSalesUnitPrice
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExActualSalesUnitPrice
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class PumpTest
    {
        [JsonProperty("@r10Ex:Type")]
        public string R10ExType { get; set; }
        public PumpTestVolume PumpTestVolume { get; set; }
        public string PumpTestReasonCode { get; set; }
        public string PumpTesterID { get; set; }
        public string PumpTestAmount { get; set; }
        public string ReturnTankID { get; set; }

        [JsonProperty("r10Ex:MerchandiseHierarchy")]
        public R10ExMerchandiseHierarchy R10ExMerchandiseHierarchy { get; set; }

        [JsonProperty("r10Ex:POSIdentity")]
        public R10ExPOSIdentity R10ExPOSIdentity { get; set; }

        [JsonProperty("r10Ex:ItemID")]
        public string R10ExItemID { get; set; }

        [JsonProperty("r10Ex:RegularSalesUnitPrice")]
        public R10ExRegularSalesUnitPrice R10ExRegularSalesUnitPrice { get; set; }

        [JsonProperty("r10Ex:ActualSalesUnitPrice")]
        public R10ExActualSalesUnitPrice R10ExActualSalesUnitPrice { get; set; }

        [JsonProperty("r10Ex:ControllerSequenceNumber")]
        public string R10ExControllerSequenceNumber { get; set; }


    }


    public class ForeCourtTransaction
    {
        [JsonProperty("@r10Ex:Type")]
        public string R10ExType { get; set; }
        public string DispenserID { get; set; }
        public string FuelPositionID { get; set; }
        public string NozzleID { get; set; }
        public PumpTest PumpTest { get; set; }

        public object FuelingPointID { get; set; }

        [JsonProperty("r10Ex:NumberOfTanks")]
        public string R10ExNumberOfTanks { get; set; }

        [JsonProperty("r10Ex:TankReading")]
        public List<R10ExTankReading> R10ExTankReading { get; set; }

        [JsonProperty("r10Ex:FuelingPointTotals")]
        public List<R10ExFuelingPointTotals> R10ExFuelingPointTotals { get; set; }

    }

    public class FuelLevel
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class WaterLevel
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class FuelVolume
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class WaterVolume
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }
    public class TcVolume
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Temperature
    {
        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Ullage
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class FuelWeight
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Tankcapacity
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExTankReading
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string ItemID { get; set; }
        public TankID TankID { get; set; }
        public string TankStatus { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public DateTime TankGaugeReadDateTime { get; set; }
        public FuelLevel FuelLevel { get; set; }
        public WaterLevel WaterLevel { get; set; }
        public FuelVolume FuelVolume { get; set; }
        public WaterVolume WaterVolume { get; set; }
        public TcVolume TcVolume { get; set; }
        public Temperature Temperature { get; set; }
        public Ullage Ullage { get; set; }
        public FuelWeight FuelWeight { get; set; }
        public string Density { get; set; }
        public string DeliveryInProgressFlag { get; set; }
        public string IsOnlineFlag { get; set; }
        public string FuelActiveReadNumber { get; set; }
        public string FuelShiftNumber { get; set; }
        public Tankcapacity Tankcapacity { get; set; }
    }

    public class MeterVolume
    {
        [JsonProperty("@Units")]
        public string Units { get; set; }

        [JsonProperty("@UnitOfMeasureCode")]
        public string UnitOfMeasureCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }
    public class R10ExFuelingPointTotals
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string ItemID { get; set; }
        public string FuelingPointID { get; set; }
        public string NozzleID { get; set; }
        public TankID TankID { get; set; }
        public string GradeID { get; set; }
        public MeterVolume MeterVolume { get; set; }
        public string FuelActiveReadNumber { get; set; }
        public string FuelShiftNumber { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public string FuellingPointStatus { get; set; }
        public string NozzleStatus { get; set; }
    }

    public class ApprovalDateTime
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class PolicyRule
    {
        [JsonProperty("@RuleType")]
        public string RuleType { get; set; }

        [JsonProperty("@EntryMethod")]
        public string EntryMethod { get; set; }
        public BusinessAction BusinessAction { get; set; }
        public ApproverID ApproverID { get; set; }
    }

    public class Policy
    {
        [JsonProperty("@PolicyID")]
        public string PolicyID { get; set; }
        public PolicyRule PolicyRule { get; set; }
    }

    public class PolicyRuleManager
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public Policy Policy { get; set; }
    }
    //public class Name
    //{
    //    public List<object> Name { get; set; }
    //}

    public class Vehicle
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string LicensePlateID { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string AdditionalInfo { get; set; }
        public string UniqueLoyaltyTransactionID { get; set; }
    }

    public class TelephoneNumber
    {
        public string AreaCode { get; set; }
        public string LocalNumber { get; set; }
    }

    public class R10ExFormKeyValueType
    {
        [JsonProperty("r10Ex:Key")]
        public string R10ExKey { get; set; }

        [JsonProperty("r10Ex:Value")]
        public string R10ExValue { get; set; }
    }

    public class R10ExFormsType
    {
        [JsonProperty("r10Ex:Name")]
        public string R10ExName { get; set; }

        [JsonProperty("r10Ex:FormType")]
        public string R10ExFormType { get; set; }

        [JsonProperty("r10Ex:FormKeyValueType")]
        public List<R10ExFormKeyValueType> R10ExFormKeyValueType { get; set; }
    }

    public class R10ExForms
    {
        [JsonProperty("r10Ex:FormsType")]
        public R10ExFormsType R10ExFormsType { get; set; }
    }

    public class BusinessDayDate
    {
        public string Date { get; set; }
    }

    public class TransactionLink
    {
        public string BusinessUnit { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<WorkstationID>))]
        public WorkstationID WorkstationID { get; set; }
        public BusinessDayDate BusinessDayDate { get; set; }
        public string SequenceNumber { get; set; }
        public string TransactionID { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<BeginDateTime>))]
        public BeginDateTime BeginDateTime { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<EndDateTime>))]
        public EndDateTime EndDateTime { get; set; }

        [JsonProperty("@ReasonCode")]
        public string ReasonCode { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }

        public string LineItemSequenceNumber { get; set; }

    }

    public class CustomerOrderTransaction
    {
        [JsonProperty("@r10Ex:VoidedFlag")]
        public string R10ExVoidedFlag { get; set; }

        [JsonProperty("@TransactionStatus")]
        public string TransactionStatus { get; set; }
        public object LineItem { get; set; }
        public List<object> Total { get; set; }
        public Customer Customer { get; set; }

        [JsonProperty("@r10Ex:VoidReasonCodeFlag")]
        public string R10ExVoidReasonCodeFlag { get; set; }

        [JsonProperty("r10Ex:Forms")]
        public R10ExForms R10ExForms { get; set; }

        [JsonProperty("@r10Ex:ResumedFlag")]
        public string R10ExResumedFlag { get; set; }
        public TransactionLink TransactionLink { get; set; }

        [JsonProperty("r10Ex:VenueShift")]
        public R10ExVenueShift R10ExVenueShift { get; set; }

        [JsonProperty("@r10Ex:TransactionType")]
        public string R10ExTransactionType { get; set; }
        public LoyaltyAccount LoyaltyAccount { get; set; }
    }

    public class TenderOpenBalanceTotalAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("@r10Ex:OverShort")]
        public string R10ExOverShort { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TenderCloseBalanceTotalAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("@r10Ex:OverShort")]
        public string R10ExOverShort { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TenderLoanTotalAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TenderPickupTotalAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TenderDepositTotalAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TenderReceiptTotalAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }
    //boomer.
    public class TenderID
    {
        [JsonProperty("@xmlns")]
        public String xmlns;
        [JsonProperty("#text")]
        public String text;
    }
    //boomer.
    public class Over
    {
        [JsonProperty("@TenderType")]
        public string TenderType { get; set; }
        public Amount Amount { get; set; }
        //public string TenderID { get; set; }
        public TenderID TenderID { get; set; }
    }

    public class Short
    {
        [JsonProperty("@TenderType")]
        public string TenderType { get; set; }
        public Amount Amount { get; set; }
        public TenderID TenderID { get; set; }
    }

    public class OverShort
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Over>))]
        public List<Over> Over { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Short>))]
        public List<Short> Short { get; set; }
    }

    public class R10ExAccount
    {
        [JsonProperty("@ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class SafeSettle
    {
        public WorkstationID WorkstationID { get; set; }
        public TenderOpenBalanceTotalAmount TenderOpenBalanceTotalAmount { get; set; }
        public TenderCloseBalanceTotalAmount TenderCloseBalanceTotalAmount { get; set; }
        public TenderLoanTotalAmount TenderLoanTotalAmount { get; set; }
        public string TotalTenderLoanCount { get; set; }
        public TenderPickupTotalAmount TenderPickupTotalAmount { get; set; }
        public string TotalTenderPickupCount { get; set; }
        public TenderDepositTotalAmount TenderDepositTotalAmount { get; set; }
        public TenderReceiptTotalAmount TenderReceiptTotalAmount { get; set; }
        public OverShort OverShort { get; set; }

        [JsonProperty("r10Ex:Account")]
        public R10ExAccount R10ExAccount { get; set; }

        [JsonProperty("r10Ex:Reason")]
        public string R10ExReason { get; set; }
    }

    public class TenderControlTransaction
    {
        [JsonConverter(typeof(SingleValueArrayConverter<PaidIn>))]
        public List<PaidIn> PaidIn { get; set; }
        public List<PaidOut> PaidOut { get; set; }
        public Account Account { get; set; }

        [JsonProperty("r10Ex:Account")]
        public R10ExAccount R10ExAccount { get; set; }
        public List<SafeSettle> SafeSettle { get; set; }
        public R10ExTotals R10ExTotals { get; set; }

        public TenderDeclaration TenderDeclaration { get; set; }
        public OverShort OverShort { get; set; }

        public TillSettle TillSettle { get; set; }
    }

    public class R10ExSettlementDate
    {
        [JsonProperty("@r10Ex:Offset")]
        public string R10ExOffset { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class InventoryLoss
    {
        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }
        public string ItemID { get; set; }
        public Quantity Quantity { get; set; }
        public string Reason { get; set; }
    }

    public class InventoryControlTransaction
    {
        public InventoryLoss InventoryLoss { get; set; }
        public TransactionLink TransactionLink { get; set; }
    }

    public class Reason
    {
        [JsonProperty("@Name")]
        public string Name { get; set; }
        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class PaidIn
    {
        [JsonConverter(typeof(SingleValueArrayConverter<Amount>))]
        public Amount Amount { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Reason>))]
        public Reason Reason { get; set; }
        public string TenderID { get; set; }
    }
    public class PaidOut
    {
        [JsonConverter(typeof(SingleValueArrayConverter<Amount>))]
        public Amount Amount { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Reason>))]
        public Reason Reason { get; set; }
        public string TenderID { get; set; }
    }
    public class BlindPickup
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string DropAmount { get; set; }
        public string TenderId { get; set; }
        public string EnvelopeId { get; set; }
        public string DropNumber { get; set; }
    }


    public class ReturnPolicyId
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ReturnPolicy
    {
        public ReturnPolicyId ReturnPolicyId { get; set; }
    }

    public class Coupon
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string SeriesID { get; set; }
        public string OfferId { get; set; }
        public string Amount { get; set; }
        public string Quantity { get; set; }
    }
    public class Return
    {
        public POSIdentity POSIdentity { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<MerchandiseHierarchy>))]
        public List<MerchandiseHierarchy> MerchandiseHierarchy { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<ItemID>))]
        public ItemID ItemID { get; set; }
        public List<Description> Description { get; set; }
        public RegularSalesUnitPrice RegularSalesUnitPrice { get; set; }
        public ActualSalesUnitPrice ActualSalesUnitPrice { get; set; }
        public ExtendedAmount ExtendedAmount { get; set; }
        public Quantity Quantity { get; set; }
        public ReturnPolicy ReturnPolicy { get; set; }
        public string Reason { get; set; }

        [JsonProperty("@ItemType")]
        public string ItemType { get; set; }
        public Disposal Disposal { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<RetailPriceModifier>))]
        public List<RetailPriceModifier> RetailPriceModifier { get; set; }
        public TransactionLink TransactionLink { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Tax>))]
        public List<Tax> Tax { get; set; }

        [JsonProperty("PromotionDeferredRewards")]
        [JsonConverter(typeof(SingleValueArrayConverter<PromotionDeferredRewards>))]
        public List<PromotionDeferredRewards> PromotionDeferredRewards { get; set; }
        public List<PromotionTenderRewards> PromotionTenderRewards { get; set; }
    }



    public class NameClass
    {
       [JsonProperty("Name")]
        public List<object> Name { get; set; }
    }

  
    public class Name
    {
        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Territory
    {
        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Address
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public Territory Territory { get; set; }
        public string PostalCode { get; set; }
    }

    public class Sale3
    {
        public POSIdentity POSIdentity { get; set; }
        public List<Description> Description { get; set; }
        public Quantity Quantity { get; set; }
        public ActualSalesUnitPrice ActualSalesUnitPrice { get; set; }
        public Kit2 Kit { get; set; }
        public List<MerchandiseHierarchy> MerchandiseHierarchy { get; set; }
        public ItemID ItemID { get; set; }
        public RegularSalesUnitPrice RegularSalesUnitPrice { get; set; }
        public ExtendedAmount ExtendedAmount { get; set; }
    }

    public class Member2
    {
        [JsonProperty("@r10Ex:GroupID")]
        public string R10ExGroupID { get; set; }

        [JsonProperty("@r10Ex:GroupTag")]
        public string R10ExGroupTag { get; set; }

        [JsonProperty("@r10Ex:IsAvailable")]
        public string R10ExIsAvailable { get; set; }
        public Sale Sale { get; set; }

        [JsonProperty("@Action")]
        public string Action { get; set; }

        [JsonProperty("@r10Ex:IsKit")]
        public string R10ExIsKit { get; set; }
    }

    public class Kit2
    {
        public List<Member2> Member { get; set; }
    }

    public class OriginalFaceAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ForeignCurrency
    {
        public DateTime DateTime { get; set; }
        public string CurrencyCode { get; set; }
        public OriginalFaceAmount OriginalFaceAmount { get; set; }
        public string ExchangeRate { get; set; }
    }

    public class TaxAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class ExemptTaxAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class TaxPerUnit
    {
        [JsonProperty("@SequenceNumber")]
        public string SequenceNumber { get; set; }
        public TaxableAmount TaxableAmount { get; set; }
        public TaxAmount TaxAmount { get; set; }
        public ExemptTaxAmount ExemptTaxAmount { get; set; }
    }

    public class ConsumableGroup
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string Type { get; set; }
        public string ID { get; set; }
    }

    public class ScanData
    {
        [JsonProperty("@TypeCode")]
        public string TypeCode { get; set; }

        [JsonProperty("@r10Ex:SubType")]
        public string R10ExSubType { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class PromotionTenderRewards
    {
        [JsonProperty("@xmlns:xsd")]
        public string XmlnsXsd { get; set; }

        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string PromotionID { get; set; }
        public string Description { get; set; }
        public LineItemRewardPromotion LineItemRewardPromotion { get; set; }
        public string SequenceNumber { get; set; }
    }

    public class R10ExGrossSales
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExRetailSummary
    {
        [JsonProperty("r10Ex:TransactionCount")]
        public string R10ExTransactionCount { get; set; }

        [JsonProperty("r10Ex:GrossSales")]
        public R10ExGrossSales R10ExGrossSales { get; set; }

        [JsonProperty("r10Ex:GST")]
        public string R10ExGST { get; set; }
    }

    public class R10ExGrossAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExAmount
    {
        [JsonProperty("@Currency")]
        public string Currency { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExTotal
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("r10Ex:Amount")]
        public R10ExAmount R10ExAmount { get; set; }

        [JsonProperty("r10Ex:Count")]
        public string R10ExCount { get; set; }

        [JsonProperty("r10Ex:GST")]
        public string R10ExGST { get; set; }
    }
    public class R10ExCashOfficeSummary
    {
        [JsonProperty("r10Ex:TransactionCount")]
        public string R10ExTransactionCount { get; set; }

        [JsonProperty("r10Ex:GrossAmount")]
        public R10ExGrossAmount R10ExGrossAmount { get; set; }

        [JsonProperty("r10Ex:GST")]
        public string R10ExGST { get; set; }

        [JsonProperty("r10Ex:Total")]
        public List<R10ExTotal> R10ExTotal { get; set; }
    }

    public class R10ExStoreEOD
    {
        [JsonProperty("r10Ex:FinanceDate")]
        public string R10ExFinanceDate { get; set; }

        [JsonProperty("r10Ex:RetailSummary")]
        public R10ExRetailSummary R10ExRetailSummary { get; set; }

        [JsonProperty("r10Ex:CashOfficeSummary")]
        public R10ExCashOfficeSummary R10ExCashOfficeSummary { get; set; }
    }
    public class Source
    {
        [JsonProperty("@ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Destination
    {
        [JsonProperty("@ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Account
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Source>))]
        public Source Source { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<Destination>))]
        public Destination Destination { get; set; }
    }

    public class R10ExTotalTaxAmount
    {
        [JsonProperty("@Type")]
        public string Type { get; set; }

        [JsonProperty("@CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExTotalTaxExclusiveAmount
    {
        [JsonProperty("@CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExTotalTaxInclusiveAmount
    {
        [JsonProperty("@CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class R10ExTotals
    {
        [JsonProperty("r10Ex:TotalTaxAmount")]
        public R10ExTotalTaxAmount R10ExTotalTaxAmount { get; set; }

        [JsonProperty("r10Ex:TotalTaxExclusiveAmount")]
        public R10ExTotalTaxExclusiveAmount R10ExTotalTaxExclusiveAmount { get; set; }

        [JsonProperty("r10Ex:TotalTaxInclusiveAmount")]
        public R10ExTotalTaxInclusiveAmount R10ExTotalTaxInclusiveAmount { get; set; }

        [JsonProperty("@xmlns:r10Ex")]
        public string XmlnsR10Ex { get; set; }

        [JsonProperty("r10Ex:Total")]
        public R10ExTotal R10ExTotal { get; set; }

      
    }

    //Boomer
    public class TotalsTenderDeclaration
    {
        [JsonProperty("Amount")]
        public Amount Amount;

        [JsonProperty("TenderID")]
        public string TenderID;

        [JsonProperty("_xmlns")]
        public string Xmlns;

        [JsonProperty("__prefix")]
        public string Prefix;

    }

    //Boomer
    /*  public class AccountTenderDeclaration
      {
          [JsonProperty("@ExternalId")]
          public string ExternalId;

          [JsonProperty("text")]
          public string Text;
      }*/


    public class TenderDeclaration
    {
        [JsonProperty("@xmlns")]
        public string Xmlns { get; set; }
        public List<Total> Totals { get; set; }

        [JsonProperty("Account", NullValueHandling = NullValueHandling.Ignore)]
        public R10ExAccount Account1 { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        [JsonProperty("r10Ex:Totals", NullValueHandling =NullValueHandling.Ignore)]
        public TotalsTenderDeclaration Total1;
    }

    public class Ending
    {
        public Amount Amount { get; set; }
        public string Count { get; set; }
    }

    public class TillSettle
    {
        public TenderSummary TenderSummary { get; set; }
        public Loans Loans { get; set; }
        public PaidIn PaidIn { get; set; }
        public PaidOut PaidOut { get; set; }
        public TenderPickup TenderPickup { get; set; }
        public OverShort OverShort { get; set; }

        [JsonProperty("r10Ex:Account")]
        public R10ExAccount R10ExAccount { get; set; }

        [JsonProperty("r10Ex:Reason")]
        public string R10ExReason { get; set; }
    }

}
