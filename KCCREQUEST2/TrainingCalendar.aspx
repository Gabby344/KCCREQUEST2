<%@ Page Language="C#"  MasterPageFile="~/NewRequest.Master"  AutoEventWireup="true" CodeBehind="TrainingCalendar.aspx.cs" Inherits="KCCREQUEST2.TrainingCalendar"%>

<%--<form runat="server">--%>
    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
<%--<script src="assets/js/jquery.MultiFile.js"></script>--%>
<!-- Bootstrap 3.3.6 -->
<script src="bootstrap/js/bootstrap.min.js"></script>
<!-- Select2 -->
<script src="plugins/select2/select2.full.min.js"></script>
<!-- InputMask -->
<script src="plugins/input-mask/jquery.inputmask.js"></script>
<script src="plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
<script src="plugins/input-mask/jquery.inputmask.extensions.js"></script>
<!-- date-range-picker -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
<script src="plugins/daterangepicker/daterangepicker.js"></script>

<%--<link rel="stylesheet" href="dist/css/AdminLTE.min.css">--%>
        <link href="Pluginss/AdminLTE.min.css" rel="stylesheet" />

<script src="fullcalendar/fullcalendar.min.js"></script>
<script src="plugins/datepicker/bootstrap-datepicker.js"></script>
<link href='FullCalendar/fullcalendar.css' rel='stylesheet' />
<link href='FullCalendar/fullcalendar.print.css' rel='stylesheet' media='print' />
<script src="Scripts/jquery.MultiFile.js"></script>


    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Calendar
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Calendar</li>
        </ol>
    </section>


   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>--%>
        
    <!-- Main content -->
    <section class="content">
        <div class="row">
        <a href="New_TrainingRequest.aspx?TrainingType=External">

 <span class="label" style="background-color: #312d2e; color: white">External Training</span></a>
       
   
            <a href="?codeTraining=27"><span class="label" style="background-color: #00649e; color: white">Onboarding Training Centre</span></a>
            <a href="?codeTraining=2"><span class="label" style="background-color: #2350cd; color: white">Luilu Training Centre</span></a>
            <a href="?codeTraining=7" ><span class="label" style="background-color: #9700fd; color: white;">  KOV Training Center</span></a>
            <a href="?codeTraining=4" ><span  class="label" style="background-color: #bea791; color: white;" >KTO Training Centre</span></a>
            <a href="?codeTraining=6" ><span  class="label" style="background-color: #00afaa; color: white;" >KTC Training Centre</span></a>
            <a href="?codeTraining=11" ><span class="label" style="background-color: #00649e; color: white;">  Refinery Training Centre</span></a>
            <a href="?codeTraining=3" ><span class="label" style="background-color: #763f83; color: white">Trades Training Centre</span></a>
            <a href="?codeTraining=14" ><span class="label" style="background-color: #dd0068; color: white;">   Security Training Center</span></a>
            <a href="?codeTraining=26" ><span class="label" style="background-color: #bc3c17; color: white;">Training Administration Building</span></a>
            <a href="?codeTraining=22" ><span class="label" style="background-color: #30903a; color: white;">Programs Delivery</span></a>

          
            <!-- /.col -->
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body no-padding">
                        <script>
                            try {


                                $(document).ready(function () {

                                    var dateObj = new Date();
                                    var month = dateObj.getUTCMonth() + 1; //months from 1-12
                                    var day = dateObj.getUTCDate();
                                    var year = dateObj.getUTCFullYear();

                                    var shiftdate = month + '/' + day + '/' + year;

                                    //  alert(shiftdate);

                                    var MonthType = 'CurrentMonth';

                                    load(shiftdate);
                                });


                                function load(responseDate) {
                                    $('div[id*=calendar]').fullCalendar({
                                        theme: false,
                                        header: {
                                            left: 'prev,next today',
                                            center: 'title',
                                            right: 'month,agendaWeek,agendaDay'
                                        },
                                        defaultView: 'month',
                                        allDay: false,
                                        editable: false,
                                        eventLimit: false,
                                        slotMinutes: 30,
                                        firstDay: 1,
                                        windowResizeDelay: 100,
                                        eventLimit: false,

                                        events: function (start, end, timezone, callback) {
                                            //debugger;            
                                            var d = $('div[id*=calendar]').fullCalendar('getDate');
                                            var responseDate = moment(d).format('MM/DD/YYYY');

                                            $.ajax({
                                                type: "POST",
                                                contentType: "application/json",
                                                data: "{'shiftdate':'" + responseDate + "'}",
                                                url: "TrainingCalendar.aspx/GetEventsMonthWise",
                                                dataType: "json",
                                                success: function (doc) {
                                                    //debugger;
                                                    var my_events = [];
                                                    if (!doc.result) {
                                                        $.each(doc.d, function (index, elem) {
                                                            //debugger;
                                                            my_events.push({
                                                                title: elem.EventName,
                                                                start: elem.StartDate,
                                                                end: elem.EndDate,
                                                                url: elem.Url,

                                                                airline: elem.Airline,
                                                                seat: elem.Seat,
                                                                color: elem.Airlinecolor,
                                                                venueName: elem.venueName
                                                            });
                                                        });
                                                        callback(my_events);
                                                    }
                                                }
                                            });
                                        }, eventRender: function (event, eventElement) {
                                            //                          debugger;

                                            eventElement.css('background-color', event.airline);

                                            if ((event.seat == "0")) {
                                                eventElement.css('background-color', 'red');
                                                event.Url = "ho";

                                            }

                                        },

                                        eventMouseover: function (data, event, view) {

                                            tooltip = '<div class="tooltiptopicevent" style="width:auto;height:auto;background:#feb811;position:absolute;z-index:10001;padding:10px 10px 10px 10px ;  line-height: 200%;">' + 'Training ' + ': ' + data.title + '</br>' + 'Available seats ' + ': ' + data.seat + '</br>' + 'Training venue ' + ': ' + data.venueName + '</div>';


                                            $("body").append(tooltip);
                                            $(this).mouseover(function (e) {
                                                $(this).css('z-index', 10000);
                                                $('.tooltiptopicevent').fadeIn('500');
                                                $('.tooltiptopicevent').fadeTo('10', 1.9);
                                            }).mousemove(function (e) {
                                                $('.tooltiptopicevent').css('top', e.pageY + 10);
                                                $('.tooltiptopicevent').css('left', e.pageX + 20);
                                            });


                                        },
                                        eventMouseout: function (data, event, view) {
                                            $(this).css('z-index', 8);

                                            $('.tooltiptopicevent').remove();

                                        },
                                    });
                                    $('.fc-prev-button').click(function () {
                                        var action = "prevMonth";
                                        var view = $('div[id*=calendar]').fullCalendar('getView');
                                        var strView = view.name;
                                        var prevMonthDate = view.start;
                                        // alert(prevMonthDate);
                                        var d = new Date(prevMonthDate);

                                        var str = moment(d).format('MM/DD/YYYY');
                                        //                        alert(str);
                                        var MonthType = 'PrevMonth';

                                        load(str);

                                    });

                                    $('.fc-next-button').click(function () {
                                        //debugger;
                                        var action = "nextMonth";
                                        var view = $('div[id*=calendar]').fullCalendar('getView');
                                        var strView = view.name;
                                        var nextMonthDate = view.start;
                                        // alert(nextMonthDate);
                                        var d = new Date(nextMonthDate);

                                        var str = moment(d).format('MM/DD/YYYY');

                                        var MonthType = 'NextMonth';
                                        // alert(str);
                                        load(str);

                                    });




                                    //$('div[id*=fullcal]').show();
                                };
                                function GetImage(type) {
                                    if (type == 0) {
                                        return "<br/><img src = 'Styles/Images/attendance.png' style='width:24px;height:24px'/><br/>"
                                    }
                                    else if (type == 1) {
                                        return "<br/><img src = 'Styles/Images/not_available.png' style='width:24px;height:24px'/><br/>"
                                    }
                                    else
                                        return "<br/><img src = 'Styles/Images/not_available.png' style='width:24px;height:24px'/><br/>"
                                }

                                //$('div[id*=fullcal]').show();



                            } catch (e) {

                                alert(e);
                                console.log(e);
                            }


                        </script>
                         <div id="loading">
                           <%-- <img src="FullCalendar/loading_wh.gif" />--%>

                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <ContentTemplate>--%>
                                <%--<fieldset>--%>
                                   
                        </div>
                                    <div id="calendar" ></div>

                                     <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                        <h4 class="modal-title">
                            <asp:Label ID="lblTitle" Text="Response From FBS" runat="server"></asp:Label>
                        </h4>
                    </div>
                   
                    <div class="modal-body" style="height: 170px; overflow: auto" runat="server" id="divmessage"> 

                        <p>
                            <asp:Label ID="lblMsg" runat="server" ></asp:Label><br>
                            <asp:Label ID="lblMsg2" runat="server" Visible="false"></asp:Label><br>
                           <%-- <asp:Label ID="lblMsg3" runat="server" Visible="false"></asp:Label><br>--%>
                            <asp:Label ID="lblMsg4" runat="server" Visible="false"></asp:Label><br>
                            <asp:Label ID="lblMsg5" runat="server" Visible="false"></asp:Label><br>
                            <asp:Label ID="lblMsg6" runat="server" Visible="false"></asp:Label><br>
                            <asp:Label ID="lblMsg7" runat="server" Visible="false" ></asp:Label><br>
                        </p>

                    </div>
                 


                    <div class="modal-footer">
                       
                        <asp:Button ID="Button1" runat="server" Text="Close" CausesValidation="false" OnClientClick="closewin()" OnClick="Button1_Click" class="btn btn-primary" Visible="true" />
                    </div>
                </div>
            </div>
        </div>
                               
                              <script type="text/javascript">

                                  function openModal() {
                                      $('#myModal').modal('show');
                                  }
                                  function closewin() {
                                      window.open('', '_self', '');
                                      window.close();
                                  }
                              </Script>

                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /. box -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </section>
    <!-- /.content -->


    <%--</form>--%>
        </asp:Content>






