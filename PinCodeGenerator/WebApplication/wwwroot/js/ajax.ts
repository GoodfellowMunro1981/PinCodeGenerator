namespace PinGenerator {

    export interface AjaxOptions {
        url: string;
        method?: string;
        data: Object;
    }

    export class Ajax {

        public async ajax(options: AjaxOptions): Promise<any> {

            return await this._sendRequest(options);
        }

        private async _sendRequest(options: AjaxOptions): Promise<any> {

            let result = await $.ajax({
                url: options.url,
                type: "POST",
                data: options.data,
                cache: false,
                success: function (data: any) {
                    return data;
                },
                error: function (result: JQueryXHR) {

                    // handle error with dialog to notify user
                }
            });

            return result;
        }
    }

}