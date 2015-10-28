
$(document).ready(function () {

    $('svg#svg1 g rect').click(function (event) {
        var rectId = event.target.id;

        var color = $('#' + rectId + '').attr('fill');
        if (color == "#D0D0D0" || color === undefined) {
            alert("This ticket is booked!");
        }
        else {

            d3.select('rect#' + rectId + '').attr("fill", "#D0D0D0");
            var recX = $('#' + rectId + '').attr('x');
            var recY = $('#' + rectId + '').attr('y');

            var svg = d3.select("svg#svg1")
                                .attr("width", 1000)
                                .attr("height", 800);

            var text = svg.append("rect:text")
                        .attr('id', 'text' + rectId.substring(3, 4) + '')
                        .attr("x", Number(recX) + 25)
                        .attr("y", Number(recY) + 25)
                        .attr("dy", ".35em")
                        .attr("text-anchor", "middle")
                        .style("font", "8 8px Helvetica Neue")
                        .style("fill", "#006699")
                        .text("" + rectId.substring(3, 4) + "");

        }
        //var bbox = text.node().getBBox();
        //d3.select('text#text1')
        //            .attr("x", bbox.x)
        //            .attr("y", bbox.y);

    })
});