// Functions for maintaining the scroll position of window when adding new task in hidden field
function SetScrollTop() {
    var windowScrollTop = $(window).scrollTop();
    $('#<%= hfScrollTop.ClientID %>').val(windowScrollTop);
}

// Scrolling to previous scroll value, saved in hidden field
$(function () {
    var scrollTop = $('#<%= hfScrollTop.ClientID %>').val();
    if (scrollTop)
        $(window).scrollTop(scrollTop);
});
