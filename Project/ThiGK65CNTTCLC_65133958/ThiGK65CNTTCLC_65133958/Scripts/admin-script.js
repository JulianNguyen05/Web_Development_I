$(document).ready(function () {
    // Xử lý sự kiện click vào nút toggle
    $('.sidebar-toggle').on('click', function () {
        // Kiểm tra kích thước màn hình
        if ($(window).width() > 768) {
            // Nếu là desktop, thu gọn/mở rộng sidebar
            $('.sidebar').toggleClass('collapsed');
            $('.main-content').toggleClass('collapsed-main');
        } else {
            // Nếu là mobile, hiện/ẩn sidebar
            $('.sidebar').toggleClass('active');
        }
    });

    // Tự động thu gọn sidebar nếu màn hình nhỏ khi tải trang
    function autoCollapseSidebar() {
        if ($(window).width() < 1200 && $(window).width() > 768) {
            $('.sidebar').addClass('collapsed');
        } else if ($(window).width() > 768) {
            $('.sidebar').removeClass('collapsed');
        }
    }

    // Chạy khi tải trang
    autoCollapseSidebar();

    // Chạy khi thay đổi kích thước cửa sổ
    $(window).on('resize', function () {
        // Cần debounce để tránh chạy liên tục
        clearTimeout(window.resizedFinished);
        window.resizedFinished = setTimeout(autoCollapseSidebar, 200);

        // Nếu resize về màn hình desktop, phải xóa lớp 'active' của mobile
        if ($(window).width() > 768) {
            $('.sidebar').removeClass('active');
        }
    });
});