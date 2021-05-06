function GetEventsOnPageLoad() {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev, next today',
            center: 'title',
            right: 'month, agendaWeek, agendaDay'
        },
        buttonText: {
            today: 'Today',
            month: 'Month',
            week: 'Week',
            day: 'Day'
            
        },
        selectable: true,
        select: function () {
            showModal('Create an Event', 'Create new Event feature coming soon.  For now, use create button in list view', null);
        },
        height: 'parent',
        events: function (start, end, timezone, callback) {
            $.ajax({
                type: "GET",
                contentType: "application/json",
                url: "GetEventData",
                dataType: "JSON",
                success: function (data) {
                    var events = [];
                    $.each(data, function (i, data) {
                        events.push(
                            {
                                title: data.title,
                                start: moment(data.start),
                                end: moment(data.end),
                                allDay: true,
                                backgroundColor: data.color,
                                id: data.id,
                                textColor: data.textColor

                                
                            }
                        ); 
                    });
                    callback(events);
                }
            })
        },
        nextDayThreshold: '00:00:00',
        editable: true,
        droppable: true,
        nowIndicator: true,
        eventClick: function (info) {
            GetFullCalEventByID(info);
        },
        eventDrop: function (info) {
            console.log(info);
            UpdateFullCalEvent(info.id, info.start.toISOString(), info.end.toISOString()); 
        },
        eventResize: function (info) {
            UpdateFullCalEvent(info.id, info.start.toISOString(), info.end.toISOString());
        }
    })
}
function GetFullCalEventByID(eventinfo) {

    $.ajax({
        type: "GET",
        url: "GetFullCalEventByID/" + eventinfo.id,
        dataType: "JSON",
        contentType: "applicaton/json; charset=utf-8",
        success: function (eventdetails) {
            showModal('Event Details', eventdetails, true);
        }
    });
}
function UpdateFullCalEvent(id, start, end) {
    var object = {};
    object.id = id;
    object.start = start;
    object.end = end;
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "UpdateFullCalEvent/",
        dataType: "JSON",
        data: JSON.stringify(object)
    });

}
function showModal(title, body, isEventDetail) {
    $("MyPopup .modal-title").html(title);

    if (isEventDetail == null) {
        $("#MyPopup .modal-body").html(body);
        $("#MyPopup").modal("show");
    }
    else {
        var title = 'Title: ' + body.title + '</br>';
        var details = 'Details: ' + body.details + '</br>';
        var date = 'Date: ' + moment(body.start).format("M/D/YYYY") + '</br>';
        var empName = 'Employee: ' + body.employeeName + '</br>';
         url = 'Location: ' + body.url + '</br>';
        var modalPop = $("#MyPopup .modal-body");

        modalPop.html(title + details + date + empName + url);
        $("#MyPopup.modal").modal("show");
    }
}