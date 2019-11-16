namespace PinGenerator {

    var site: Site;

    $(document).ready(function () {
        site = new Site();
    });

    export interface PinResponse {
        success: boolean,
        pin: string,
        message: string
    }

    export class Site {

        private _element: JQuery;
        private _newPin: JQuery;
        private _pinDisplay: JQuery;
        private _ajax: Ajax;

        constructor() {
            this._init();
        }

        private _init(): void {
            
            this._element = $('#PinGenerator');
            this._newPin = this._element.find('.new-pin');
            this._pinDisplay = this._element.find('.generated-pin');
            this._ajax = new Ajax();
            this._setEvents();
        }

        private _setEvents(): void {

            this._newPin.click((e: JQueryEventObject) => this._getNewPin());
        }

        private async _getNewPin(): Promise<void> {

                this._pinDisplay.val("Generating Pin");

                let ajaxOptions: AjaxOptions = {
                    url: "Home/GetPin",
                    data: {
                        name: name
                    }
                };

                let response: PinResponse = await this._ajax.ajax(ajaxOptions);
                let displayValue = "Error";

                if (response.success) {
                    displayValue = response.pin;
                }
                else if (response.message != null && response.message.length > 0) {
                    displayValue = response.message;
                }

                this._pinDisplay.val(displayValue);
        }
    }
}