
var url = "/home/CreateSvgItems";
d3.json(url, function (json) {

    var canvas = d3.selectAll("#test")
    .append("svg")
    .attr("id", "svg1")
    .attr("width", 1000)
    .attr("height", 800);


    var rects = canvas
                .append('g')
                .selectAll('rect')
                .data(json)
                .enter()
                    .append('rect','1')
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
                    
})


