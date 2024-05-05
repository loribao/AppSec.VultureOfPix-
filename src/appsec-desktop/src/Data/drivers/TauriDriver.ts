import { injectable } from "tsyringe";
import { IApi, IApiRequest, IApiResponse } from "../../Domain/interfaces/IContexts/IContext";
import IDriver from "../../Domain/interfaces/IDrivers/IDriver";
import { fetch } from "@tauri-apps/plugin-http";



class Api implements IApi {
    get = async <ResData>(request: IApiRequest<void>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'GET',
            headers: request.headers,
        });
        let d = await response.json();
        return { data: d, status: response.status, statusText: response.statusText } as IApiResponse<ResData>;
    }

    post = async <ReqBody, ResData>(request: IApiRequest<ReqBody>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'POST',
            body: JSON.stringify(request.body as any),
            headers: request.headers,
        });
        let d = await response.json();
        return { data: d, status: response.status, statusText: response.statusText } as IApiResponse<ResData>;
    }
    put = async <ReqBody, ResData>(request: IApiRequest<ReqBody>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'PUT',
            body: JSON.stringify(request.body as any),
            headers: request.headers,
        });
        let d = await response.json();
        return { data: d, status: response.status, statusText: response.statusText } as IApiResponse<ResData>;
    }

    delete = async <ReqBody, ResData>(request: IApiRequest<ReqBody>): Promise<IApiResponse<ResData>> => {

        const response = await fetch(request.url, {
            method: 'delete',
            body: JSON.stringify(request.body as any),
            headers: request.headers,
        });
        let d = await response.json();
        return { data: d, status: response.status, statusText: response.statusText } as IApiResponse<ResData>;
    }
}

@injectable()
class TauriDriver implements IDriver {
    Api: IApi;
    constructor() {
        this.Api = new Api();
    }

    async isDriverAsync(): Promise<boolean> {
        return this.isDriver();
    }

    isDriver = (): boolean => ((typeof (window as any)["__TAURI_INTERNALS__"] == 'undefined') ? false : true);
}

export default TauriDriver;
