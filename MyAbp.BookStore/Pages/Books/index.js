$(function () {
    var l = abp.localization.getResource("BookStore");
	
    var bookService = myAbp.bookStore.books.book;
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Books/CreateModal",
        scriptUrl: "/Pages/Books/createModal.js",
        modalClass: "bookCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Books/EditModal",
        scriptUrl: "/Pages/Books/editModal.js",
        modalClass: "bookEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			authorName: $("#AuthorNameFilter").val(),
			priceMin: $("#PriceFilterMin").val(),
			priceMax: $("#PriceFilterMax").val(),
			publishDateMin: $("#PublishDateFilterMin").data().datepicker.getFormattedDate('yyyy-mm-dd'),
			publishDateMax: $("#PublishDateFilterMax").data().datepicker.getFormattedDate('yyyy-mm-dd')
        };
    };

    var dataTable = $("#BooksTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(bookService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('BookStore.Books.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('BookStore.Books.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    bookService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" },
			{ data: "authorName" },
			{ data: "price" },
            {
                data: "publishDate",
                render: function (publishDate) {
                    if (!publishDate) {
                        return "";
                    }
                    
					var date = Date.parse(publishDate);
                    return (new Date(date)).toLocaleDateString(abp.localization.currentCulture.name);
                }
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#fileType').select2();

    $("#NewBookButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        bookService.getDownloadToken().then(
            function (result) {
                var input = getFilter();
                var fileType = $('#fileType').val();
                var url = abp.appPath + 'api/book-store/books/get-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'name', value: input.name }, 
                            { name: 'fileType', value: fileType }, 
                            { name: 'authorName', value: input.authorName },
                            { name: 'priceMin', value: input.priceMin },
                            { name: 'priceMax', value: input.priceMax },
                            { name: 'publishDateMin', value: input.publishDateMin },
                            { name: 'publishDateMax', value: input.publishDateMax }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
});
