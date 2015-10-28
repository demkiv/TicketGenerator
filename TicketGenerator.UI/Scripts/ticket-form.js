var TicketInfo = function () {

	var eventInfo = function () {
		
		var eventId = $("#EventId option:selected").val();
		$.ajax({
			type: "get",
			url: "/Home/GetEventInfo",
			data: "eventId=" + eventId,
			dataType: "json",
			contentType: 'application/json',
			success: function (data) {
				$("#EventDate").val(data.EventDate);
				$("#Price").val(data.Price);
			},
			error: function (jqXhr, textStatus, errorThrown) {
				console.log(textStatus, errorThrown);
			}
		});

	}

	var sectorInfo = function () {

		var sectorId = $("#SectorId option:selected").val();
		$.ajax({
			type: "get",
			url: "/Home/GetSectorInfo",
			data: "sectorId=" + sectorId,
			dataType: "json",
			contentType: 'application/json',
			success: function (data) {
				console.log(data);
				
			},
			error: function (jqXhr, textStatus, errorThrown) {
				console.log(textStatus, errorThrown);
			}
		});

	}
	return {
		EventInfo : eventInfo,
		SectorInfo: sectorInfo
	};
}();