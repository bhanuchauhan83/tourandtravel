var PersonInfo = {
    //UpdateTravellerInfo: function () {

    //    $.ajax({
    //        cache: false,
    //        url: '/Cart/UpdateTravellerInfo',
    //        type: "POST",
    //        success: this.nextStep,
    //        data: $('#co-traveller-personal-info-form').serialize(),
    //        complete: this.resetLoadWaiting,
    //        error: this.ajaxFailure
    //    });

    //},
    nextStep: function (response) {
        if (response.error) {
            if (typeof response.message === 'string') {
                alert(response.message);
            } else {
                alert(response.message.join("\n"));
            }

            return false;
        }

        Checkout.setStepResponse(response);
    },
    LoadHtml: function (loadURl) {
        $.ajax({
            cache: false,
            url: loadURl,
            type: "POST",
            success: this.nextStep,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    }

}

var PersonalInfo = {
    form: false,
    url: false,
    init: function (form, url) {
        this.form = form;
        this.url = url;
    },
    save: function () {
        if (this.checkifAllValid()) {
            $.ajax({
                cache: false,
                url: this.url,
                type: "POST",
                data: $(this.form).serialize(),
                success: this.nextStep,
                complete: this.resetLoadWaiting,
                error: this.ajaxFailure
            });
        } else {
            var alertShown = false;
            $('#traveller-personal-info input').each(function () {              
                var max = $(this).attr('max');
                var min = $(this).attr('min');
                if (!alertShown && max && min) {                  
                    if (parseInt($(this).val()) > parseInt(max)) {
                        alertShown = true;
                        alert("Invalid Age max age is " + max + "");
                    } else if (parseInt($(this).val()) < parseInt(min)) {
                        alertShown = true;
                        alert("Invalid Age min age is " + min + "");
                    }
                }
            });
            if (!alertShown)
                alert("fill all required field")
        }

    },
    checkifAllValid: function () {
        var valid = true;
        $('#traveller-personal-info input').each(function () {
            if (valid === true) {
                if ($(this).attr('id').match('FirstName') !== null && $(this).val() === '') {
                    valid = false;
                } if ($(this).attr('id').match('Age') !== null && $(this).val() <= 0) {
                    valid = false;
                }
            }
        })
        return valid;
    },
    nextStep: function (response) {
      
        if (response.error) {
            if (typeof response.message === 'string') {
                alert(response.message);
            } else {
                alert(response.message.join("\n"));
            }

            return false;
        }

        Checkout.setStepResponse(response);
        Billing.initializeCountrySelect();
    },

}

var ContactInfo = {
    form: false,
    url: false,
    init: function (form, url) {
        this.form = form;
        this.url = url;
    },
    save: function () {
        $.ajax({
            cache: false,
            url: this.url,
            type: "POST",
            data: $(this.form).serialize(),
            success: this.nextStep,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },
    nextStep: function (response) {       
        if (response.error) {
            if (typeof response.message === 'string') {
                alert(response.message);
            } else {
                alert(response.message.join("\n"));
            }

            return false;
        }

        Checkout.setStepResponse(response);
    },
}