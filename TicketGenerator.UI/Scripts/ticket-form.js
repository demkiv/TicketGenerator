
var selectedItem;
var boughtItem;

var createSvg = function (data) {

    $.ajax({
        type: 'POST',
        url: "/Home/CreateSvgItems",
        traditional: true,
        dataType: 'json',
        data: data,
        success: function (data) {
            console.log(data);

            $("svg#svg1").remove();

            var rows = data[data.length - 1].svgRow + 1;
            var cols = data[data.length - 1].svgCol + 2;

            var width = cols * 50 + (cols + 1) * 10;
            var height = rows * 50 + (rows + 1) * 10;

            var canvas = d3.selectAll("#test")
            .append("svg")
            .attr("id", "svg1")
            .attr("width", width)
            .attr("height", height);


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
                        if (data.svgReserved) {
                            return '#C0C0C0'
                        } else {
                            return '#006699'
                        }
                    },

                })
                .on("click", function (data) {
                    var info = JSON.stringify(data);

                    if (this.__data__.svgId == boughtItem) {
                        this.__data__.svgReserved = true;
                        boughtItem = undefined;
                    }

                    if (this.__data__.svgReserved) {
                    	swal({ title: "This ticket is already sold!", text: "Please, pick another one.", type: "error", confirmButtonText: "Ok" });
                        $("#Row").val("");
                        $("#Number").val("");
                        $("#SeatId").val("");
                        d3.select('rect#' + selectedItem + '').attr('fill', "#006699");
                        d3.select('text#' + selectedItem + '').remove();
                    } else {

                        if (selectedItem != undefined) {
                            d3.select('rect#' + selectedItem + '').attr('fill', "#006699");
                            d3.select('text#' + selectedItem + '').remove();
                        }
                        selectedItem = this.__data__.svgId;

                        d3.select('rect#' + this.__data__.svgId + '').attr("fill", "#FFFFCC");
                        var recX = $('#' + this.__data__.svgId + '').attr('x');
                        var recY = $('#' + this.__data__.svgId + '').attr('y');

                        var svg = d3.select("svg#svg1")
                            .attr("width", width)
                            .attr("height", height);

                        var text = svg.append("rect:text")
                            .attr('id', '' + this.__data__.svgId + '')
                            .attr("x", Number(recX) + 25)
                            .attr("y", Number(recY) + 25)
                            .attr("dy", ".35em")
                            .attr("text-anchor", "middle")
                            .style("font", "8 8px Helvetica Neue")
                            .style("fill", "#006699")
                            .text("" + this.__data__.svgCol + "");
                        $("#Row").val(this.__data__.svgRow);
                        $("#Number").val(this.__data__.svgCol);
                        $("#SeatId").val(parseInt(this.__data__.svgId.substring(1, this.__data__.svgId.length)));
                    }


                });
        },
        error: function (data) { console.log(data) },
        contentType: 'application/json; charset=UTF-8',
        processData: false,
        async: false
    })

}
var createSvgRows = function (data) {

    $.ajax({
        type: 'POST',
        url: "/Home/CreateSvgRows",
        traditional: true,
        dataType: 'json',
        data: data,
        success: function (data) {
            console.log(data);

            var canvas = d3.selectAll("#svg1")

            var rects = canvas
                        .append('g')
                        .selectAll('rect')
                        .data(data)
                        .enter()

                            .append('text', '1')
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
                                    return 'black'
                                },

                            }).text(function (data, index) {
                                return data.svgId
                            })


        },
        error: function (data) { console.log(data) },
        contentType: 'application/json; charset=UTF-8',
        processData: false,
        async: false
    })

}

var TicketInfo = function () {

	var eventSectorInfo = function () {

	    var sectorId = $("#SectorId option:selected").val();
	    var eventId = $("#EventId option:selected").val();

	    $.ajax({
			type: "get",
			url: "/Home/GetEventSectorInfo",
			data: "sectorId=" + sectorId + "&eventId=" + eventId,
			dataType: "json",
			contentType: 'application/json',
			success: function (data) {
			    console.log(data);

			    $("#Row").val("");
			    $("#Number").val("");
			    $("#SeatId").val("");
			    $("#EventDate").val(data.EventDate);
			    $("#Price").val(data.Price);

			    createSvg(JSON.stringify(data));
			    createSvgRows(JSON.stringify(data));
			},
			error: function (jqXhr, textStatus, errorThrown) {
				console.log(textStatus, errorThrown);
			}
		});

	}

	var submitButtonClick = function (e) {

		e.preventDefault();
		var seatid = $("#SeatId").val();

		if (seatid === "0" || seatid === "") {
			swal({ title: "You didn't choose a seat...", text: "Please, pick one!", type: "error", confirmButtonText: "Ok" });
		} else {

			$.ajax({
				url: "/Home/TicketInfo",
				type: "post",
				data: $(this).serialize(),
				success: function (data) {

					if (data.IsSuccessfulOperation) {
						d3.select('rect#r' + data.SeatId + '').attr('fill', "#C0C0C0");
						d3.select('text#r' + data.SeatId + '').remove();

						boughtItem = 'r' + data.SeatId;
						selectedItem = undefined;

						$("#Row").val("");
						$("#Number").val("");
						$("#SeatId").val("");

						window.open("/Home/OpenPDF?id=" + data.TicketId);
					} else {
						swal({ title: "Oops... Something went wrong...", text: "Please, reload the page and try again!", type: "error", confirmButtonText: "Ok" });
					}
				},
				error: function (jqXhr, textStatus, errorThrown) {
					console.log(textStatus, errorThrown);
				}
			});

		}
	}
    

    function bind() {
		$(document).on("submit", "#ticket-form", submitButtonClick);
	}

	function unbind() {
		$(document).off("submit", "#ticket-form");
	}

	return {
		EventSectorInfo: eventSectorInfo,
		Bind: bind,
		Unbind: unbind
	};
}();




$(document).ready(function () {

	TicketInfo.Unbind();
	TicketInfo.Bind();

	createSvg([]);
	createSvgRows([]);
   
});