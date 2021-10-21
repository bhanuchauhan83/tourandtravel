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
        debugger;
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
        debugger;
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
        debugger;
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