var target = document.querySelector("body");
ErrorMessage = (e) => {
    debugger
    if (e.responseJSON.isSuccess === true) {
        location.href = e.responseJSON.ReturnUrl;
    } else {
       
        SetErrorMessage("İşleminiz Yapılamadı", e.responseJSON.Errors);
    }

  
}



SetErrorMessage = (title,message) => {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toastr-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.error(message, title);
}

SetSuccessMessage = (title,message) => {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toastr-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "onHidden":function(){
            location.reload();
        }
    };

    toastr.success(message, title);
}
 

fncFormSubmitted = (e) => { 
    debugger
    if (e.statusText=='success') {
        SetSuccessMessage("İşlem Başarılı","Kayıt Başarıyla Yapıldı");
    } else {
        SetErrorMessage("İşleminiz Yapılamadı", e.responseText);
    }
}


var blockUI = new KTBlockUI(target, {
message:'<div class="blockui-message"><span class="spinner-border text-primary"></span> Lütfen Bekleyin...</div>',
});


$(document).on({
    ajaxSetup: function () {
        cache:false
    },
    ajaxStart: function () { 
        blockUI.block();
    },
    ajaxStop: function () {
        blockUI.release();
    },
    ajaxError: function ( jqXHR, textStatus, errorThrown) {
        debugger
        blockUI.release();
        SetErrorMessage("İşleminiz Yapılamadı", textStatus.responseText);
    }
})



/// Grid Initializers
onChange = (e) => {
    var grid = $('[id^=grd]').data('kendoGrid');
    var selectedItem = grid.dataItem(grid.select());
    localStorage.setItem("selected", JSON.stringify(selectedItem));
    console.log(localStorage.getItem("selected"));
}

onDataBound = (e) => {
    var grid = $('[id^=grd]').data('kendoGrid');
    grid.tbody.find('tr').each(function (idx) {
        eval($(this).find('script').html())
    })
}
////


WindowOpen = (title,url) => {
    win = $("#window").data("kendoWindow");
    var opts = win.options;
    opts.height = "60%";
    opts.width = "40%";
    win.setOptions(opts);
    win.one("refresh", function (e) {
        e.sender.center();
    });
    win.title(title);
    win.refresh({
        url: url
    });
    win.open();
}

ModalOpen = (title, url) => {
      $.get(url, (v) => {
        $("#mdlError .modal-body").html(v); 
    }).then(() => {
        $("#mdlError").modal('show');
    })
}