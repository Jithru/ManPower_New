﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveDME22Render.aspx.cs" Inherits="ManPowerWeb.ApproveDME22Render" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div cssclass="table-responsive">
        <asp:GridView ID="gvDME22Approve" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="StartTime.Date" HeaderText="Date" />
                <asp:BoundField DataField="_TaskType.TaskTypeName" HeaderText="Work Type" />
                <asp:BoundField DataField="TaskDescription" HeaderText="Performed Duty" />
                <asp:BoundField DataField="WorkLocation" HeaderText="Work Attended place" />
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin-top: 20px; margin-bottom: 20px;">
        <asp:LinkButton ID="btnApprove" CssClass="btn btn-outline-success btn-lg btn-block" runat="server" OnClick="btnApprove_Click">Approve DME22</asp:LinkButton>
    </div>
    <div style="margin-top: 20px; margin-bottom: 20px;">
        <asp:LinkButton ID="btnReject" CssClass="btn btn-outline-danger btn-lg btn-block" runat="server" OnClick="btnReject_Click">Reject DME22</asp:LinkButton>
    </div>
</asp:Content>
