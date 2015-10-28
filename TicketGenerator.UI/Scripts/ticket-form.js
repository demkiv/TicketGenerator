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

			    $("#Row").val("");
			    $("#Number").val("");

				$.ajax({
				    type: 'POST',
				    url: "/Home/CreateSvgItems",
				    traditional: true,
				    dataType: 'json',
				    data: JSON.stringify(data),
				    success: function (data) {
				        console.log(data);

				             $("svg#svg1").remove();

				            var canvas = d3.selectAll("#test")
                            .append("svg")
                            .attr("id", "svg1")
                            .attr("width", 1000)
                            .attr("height", 800);


				            var rects = canvas
                                        .append('g')
                                        .selectAll('rect')
                                        .data(data)
                                        .enter()
                                            .append('rect', '1')
                                            .attr({
                                                'x': function (data, index) {
                                                    return data.svgX;
                                                },
                                                'y': function (data, index) {
                                                    return data.svgY;
                                                },
                                                'id': function (data, index) {
                                                    return data.svgId
                                                },
                                                'width': function (data, index) {
                                                    return 50
                                                },
                                                'height': function (data, index) {
                                                    return 50
                                                },
                                                'fill': function (data, index) {
                                                    return '#006699'
                                                },

                                            })
                                            .on("click", function (data) {
                                                var info = JSON.stringify(data);
                                                //alert(info.svgX);
                                                //d3.select(this).ajax(alert(JSON.stringify(data)));
                                                //alert(this.__data__.svgRow);
                                                $("#Row").val(this.__data__.svgCol);
                                                $("#Number").val(this.__data__.svgRow);
                                                $("#SeatId").val(parseInt(this.__data__.svgId));
                                            })
				    },
				    error: function (data) { console.log(data) },
				    contentType: 'application/json; charset=UTF-8',
				    processData: false,
				    async: false
				})
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