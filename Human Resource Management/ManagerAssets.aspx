<%@ Page Title="" Language="C#" MasterPageFile="~/Manager.Master" AutoEventWireup="true" CodeBehind="ManagerAssets.aspx.cs" Inherits="Human_Resource_Management.ManagerAssets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="main-wrapper">
     <!-- Page Wrapper -->
     <div class="page-wrapper">
         <!-- Page Content -->
         <div class="content container-fluid">

             <!-- Page Header -->
             <div class="page-header">
                 <div class="row align-items-center">
                     <div class="col">
                         <h3 class="page-title">Assets</h3>
                         <ul class="breadcrumb">
                             <li class="breadcrumb-item"><a href="Admindashboard">Dashboard</a></li>
                             <li class="breadcrumb-item active">Assets</li>
                         </ul>
                     </div>
                     <div class="col-auto float-end ms-auto">
                        <%--   <a href="AdminAddAssets.aspx" class="btn add-btn"><i class="fa-solid fa-plus"></i>Add Assets</a>
                            <a href="#" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#add_resignation"><i class="fa-solid fa-plus"></i>Add Resignation</a>--%>
                         <div class="view-icons">
                             <%-- <a href="AdminAllEmployees.aspx" class="grid-view btn btn-link"><i class="fa fa-th"></i></a>--%>
                           
                             <%-- <a href="AdminAddEmployee.aspx" class="list-view btn btn-link active"><i class="fa-solid fa-plus"></i></a>--%>
                         </div>

                     </div>
                 </div>
             </div>
             <!-- /Page Header -->

             <div class="row">
                 <div class="col-md-12">
                     <div class="table-responsive">
                         <table class="table table-striped custom-table mb-0 datatable leave-employee-table">
                             <thead>
                                 <tr>
                                     <th>Name</th>
                                     <th>Department </th>
                                     <th>LapTop</th>
                                     <th>Mouse</th>
                                     <th>PenDrive</th>
                                     <th>Mobile</th>
                                     <th>Bag</th>
                                     <th>Sim</th>
                                     <th>AssignBy</th>
                                     <th>Date</th>
                                 </tr>
                             </thead>
                             <tbody>
                                 <asp:PlaceHolder ID="AssetsData" runat="server"></asp:PlaceHolder>
                             </tbody>
                         </table>
                     </div>
                 </div>
             </div>
         </div>

         <!-- Edit Assets Modal -->
         <div id="edit_assets" class="modal custom-modal fade" role="dialog">
             <div class="modal-dialog modal-dialog-centered" role="document">
                 <div class="modal-content">
                     <div class="modal-header">
                         <h5 class="modal-title">Edit Assets</h5>
                         <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                             <span aria-hidden="true">&times;</span>
                         </button>
                     </div>
                     <div class="modal-body">
                         <asp:HiddenField ID="HiddenField1" runat="server" />                      
                         <div class="input-block mb-3">
                             <label class="col-form-label">Name <span class="text-danger">*</span></label>
                             <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                         </div>

                         <div class="input-block mb-3">
                             <label class="col-form-label">LapTop <span class="text-danger">*</span></label>
                             <asp:CheckBox ID="CheckBox1" runat="server" CssClass="form-control" />
                         </div>

                         <div class="input-block mb-3">
                             <label class="col-form-label">Mouse <span class="text-danger">*</span></label>
                             <asp:CheckBox ID="CheckBox2" runat="server" CssClass="form-control" />
                         </div>

                         <div class="input-block mb-3">
                             <label class="col-form-label">Pendrive <span class="text-danger">*</span></label>
                             <asp:CheckBox ID="CheckBox3" runat="server" CssClass="form-control" />
                         </div>

                         <div class="input-block mb-3">
                             <label class="col-form-label">Mobile <span class="text-danger">*</span></label>
                             <asp:CheckBox ID="CheckBox4" runat="server" CssClass="form-control" />
                         </div>

                         <div class="input-block mb-3">
                             <label class="col-form-label">Bag <span class="text-danger">*</span></label>
                             <asp:CheckBox ID="CheckBox5" runat="server" CssClass="form-control" />
                         </div>

                         <div class="input-block mb-3">
                             <label class="col-form-label">Sim <span class="text-danger">*</span></label>
                             <asp:CheckBox ID="CheckBox6" runat="server" CssClass="form-control" />
                         </div>
                         <div class="submit-section">
                             <asp:Button ID="btnupdateassets" runat="server" Text="Update" CssClass="btn btn-primary submit-btn" OnClick="btnupdateassets_Click" />
                         </div>
                     </div>
                 </div>
             </div>
         </div>
         <!-- /Edit Assets Modal -->

        
         <!-- /Page Content -->
     </div>
     <!-- /Page Wrapper -->

 </div>
 <!-- /Main Wrapper -->
 <script type="text/javascript">
     function editassetst(EmpId, Name, LapTop, Mouse, Pendrive, Mobile, Bag, Sim) {        
         $('#<%= HiddenField1.ClientID %>').val(EmpId);
         $('#<%= txtname.ClientID %>').val(Name);
         $('#<%= CheckBox1.ClientID %>').prop('checked', LapTop && LapTop.toLowerCase() === 'true');
         $('#<%= CheckBox2.ClientID %>').prop('checked', Mouse && Mouse.toLowerCase() === 'true');
         $('#<%= CheckBox3.ClientID %>').prop('checked', Pendrive && Pendrive.toLowerCase() === 'true');
         $('#<%= CheckBox4.ClientID %>').prop('checked', Mobile && Mobile.toLowerCase() === 'true');
         $('#<%= CheckBox5.ClientID %>').prop('checked', Bag && Bag.toLowerCase() === 'true');
         $('#<%= CheckBox6.ClientID %>').prop('checked', Sim && Sim.toLowerCase() === 'true');
         console.log("Sim is : " + Sim);
     }
 </script>
</asp:Content>
