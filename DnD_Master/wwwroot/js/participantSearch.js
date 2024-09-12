$(document).ready(function () {
    $('#participantInput').on('input', function () {
        var query = $(this).val();
        if (query.length > 0) {
            $.ajax({
                url: searchParticipantsUrl,
                type: 'GET',
                data: { searchTerm: query },
                success: function (data) {
                    $('#suggestions').empty();
                    if (data.length > 0) {
                        $.each(data, function (index, participant) {
                            $('#suggestions').append($('<li class="list-group-item">').text(participant.name));
                        });
                        $('#suggestions').show();
                    } else {
                        $('#suggestions').hide();
                    }
                },
                error: function () {
                    alert('Error fetching participants');
                }
            });
        } else {
            $('#suggestions').hide();
        }
    });

    // Убирает список если кликнуть вне выподающего списка
    $(document).on('click', function (e) {
        if (!$(e.target).closest('#participantInput').length) {
            $('#suggestions').hide();
        }
    });

    // Заполняет строку выброным значением при клике на него
    $(document).on('click', '.list-group-item', function () {
        $('#participantInput').val($(this).text());
        $('#suggestions').hide();
    });

// TO DO: Разобратся как сделать подсветку выбора элемента
// // Подсветка элемента списка при наведении
// $(document).on('mouseenter', '.list-group-item', function () {
//     $(this).addClass('highlight');
// }).on('mouseleave', '.list-group-item', function () {
//     $(this).removeClass('highlight');
// });
});
