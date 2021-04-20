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
            showModal('Create an Event', 'Bind your information to navigate any page', null);
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
                                id: data.id
                            });
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
            GetEventDetailByEventId(info);
        },
        eventDrop: function (info) {
            console.log(info);
            UpdateEventDetails(info.id, info.start.toISOString(), info.end.toISOString());
        },
        eventResize: function (info) {
            UpdateEventDetails(info.id, info.start.toIOSString(), info.end.toIOSString());
        }
    })
}
function showModal(title, body, isEventDetail) {
    $("MyPopup .modal-title").html(title);
}