﻿using ManPowerCore.Common;
using ManPowerCore.Controller;
using ManPowerCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManPowerWeb
{
    public partial class ApproveLoanAdmin1 : System.Web.UI.Page
    {
        public int loanDetailsId;
        public List<LoanDetail> loanDetailList = new List<LoanDetail>();
        public LoanDetail loanDetailObj = new LoanDetail();
        public List<LoanType> loanTypeList = new List<LoanType>();
        public DistressLoan distressLoanObj = new DistressLoan();
        public List<GuarantorDetail> guarantordetailList = new List<GuarantorDetail>();
        public List<RequestorGuarantor> requestorGuarantorList = new List<RequestorGuarantor>();
        public ApprovalHistory approvalHistoryObj = new ApprovalHistory();
        public int EmpId;

        LoanDetailsController loanDetailsController = ControllerFactory.CreateLoanDetailsController();
        DistressLoanController distressLoanController = ControllerFactory.CreateDistressLoanController();
        GuarantorDetailController guarantorDetailController = ControllerFactory.CreateGuarantorDetailController();
        RequestorGuarantorController requestorGuarantorController = ControllerFactory.CreateRequestorGuarantorController();
        ApprovalHistoryController approvalHistoryController = ControllerFactory.CreateApprovalHistoryController();

        protected void Page_Load(object sender, EventArgs e)
        {
            EmpId = Convert.ToInt32(Session["EmpNumber"]);
            loanDetailsId = Convert.ToInt32(Request.QueryString["LoanDetailId"]);
            BindDataSource();
        }

        public void BindDataSource()
        {

            loanDetailList = loanDetailsController.GetAllLoanDetailWithStatus(true, true);

            loanDetailObj = loanDetailList.Where(x => x.LoanDetailsId == loanDetailsId).Single();

            BindDdlLoanType();

            ddlLoanType.SelectedValue = loanDetailObj.LoanTypeId.ToString();
            txtName.Text = loanDetailObj.FullName;
            txtPosition.Text = loanDetailObj.Position;
            txtPositionType.Text = loanDetailObj.WorkType;
            txtWorkPlace.Text = loanDetailObj.WorkPlace;
            txtAppointmentDate.Text = loanDetailObj.AppointedDate.ToString("yyyy-MM-dd");
            txtBasicSalary.Text = loanDetailObj.BasicSalary.ToString();
            txtLoanAmount.Text = loanDetailObj.LoanAmount.ToString();
            txtDateWanted.Text = loanDetailObj.LoanRequireDate.ToString("yyyy-MM-dd");

            if (loanDetailObj.LoanTypeId.ToString() == "3")
            {
                distressLoanObj = distressLoanController.GetAllDistressLoan().Where(x => x.LoanDetailsId == loanDetailsId).Single();

                txtLoanReason.Text = distressLoanObj.ReasonForLoan;
                txtLastLoan.Text = distressLoanObj.LastLoanDate.ToString("yyyy-MM-dd");

                guarantordetailList = guarantorDetailController.GetAllGuarantorDetail().Where(x => x.DistressLoanId == distressLoanObj.DistressLoanId).ToList();

                gvGuarantor.DataSource = guarantordetailList;
                gvGuarantor.DataBind();

                requestorGuarantorList = requestorGuarantorController.GetAllRequestorGuarantor().Where(x => x.DistressLoanId == distressLoanObj.DistressLoanId).ToList();

                gvApplicantAsGurontor.DataSource = requestorGuarantorList;
                gvApplicantAsGurontor.DataBind();
            }
        }

        public void BindDdlLoanType()
        {
            LoanTypeController loanTypeController = ControllerFactory.CreateLoanTypeController();

            loanTypeList = loanTypeController.GetAllLoanType();

            ddlLoanType.DataSource = loanTypeList;
            ddlLoanType.DataValueField = "Id";
            ddlLoanType.DataTextField = "Loan_Type_Name";
            ddlLoanType.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            loanDetailObj.ApprovalStatusId = 2;
            loanDetailObj.LastLoanDate = DateTime.Now;
            loanDetailObj.LastLoanPaidMonth = DateTime.Now;
            loanDetailObj.SalaryNo = "0";

            loanDetailsController.Update(loanDetailObj);

            approvalHistoryObj.ApprovalStatusId = 2;
            approvalHistoryObj.ApproveDate = DateTime.Now;
            approvalHistoryObj.ApproveBy = EmpId;
            approvalHistoryObj.LoanDetailsId = loanDetailsId;
            approvalHistoryObj.RejectReason = "";

            approvalHistoryController.Save(approvalHistoryObj);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Approved Succesfully!', 'success');window.setTimeout(function(){window.location='ApproveLoanAdmin1Front.aspx'},2500);", true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            loanDetailObj.ApprovalStatusId = 3;
            loanDetailObj.LastLoanDate = DateTime.Now;
            loanDetailObj.LastLoanPaidMonth = DateTime.Now;
            loanDetailObj.SalaryNo = "0";

            loanDetailsController.Update(loanDetailObj);

            approvalHistoryObj.ApprovalStatusId = 3;
            approvalHistoryObj.ApproveDate = DateTime.Now;
            approvalHistoryObj.ApproveBy = EmpId;
            approvalHistoryObj.LoanDetailsId = loanDetailsId;
            approvalHistoryObj.RejectReason = txtrejectReason.Text;

            approvalHistoryController.Save(approvalHistoryObj);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Success!', 'Rejected Succesfully!', 'success');window.setTimeout(function(){window.location='ApproveLoanAdmin1Front.aspx'},2500);", true);
        }
    }
}