$(document).ready(function () {

    $(".ticket_status").change(function () {
        var Id = $(this).data("itemid");
        var ticketStatus = $(this).val();
        $.post('/Ticket/SetTicketStatus', { Id: Id, ticketStatus: ticketStatus });
    });

});