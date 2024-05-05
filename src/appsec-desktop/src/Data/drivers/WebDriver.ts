import { IApi, IApiRequest, IApiResponse } from "../../Domain/interfaces/IContexts/IContext";
import IDriver from "../../Domain/interfaces/IDrivers/IDriver";


class Api implements IApi {
    get = async <ResData>(request: IApiRequest<void>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'GET',
            headers: request.headers,
        });
        const data = await response.json();
        return { data, status: response.status } as IApiResponse<ResData>;
    }

    post = async <ReqBody, ResData>(request: IApiRequest<ReqBody>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'POST',
            headers: request.headers,
            body: JSON.stringify(request.body),
        });
        const data = await response.json();
        return { data, status: response.status } as IApiResponse<ResData>;
    }

    put = async <ReqBody, ResData>(request: IApiRequest<ReqBody>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'PUT',
            headers: request.headers,
            body: JSON.stringify(request.body),
        });
        const data = await response.json();
        return { data, status: response.status } as IApiResponse<ResData>;
    }

    delete = async <ReqBody, ResData>(request: IApiRequest<ReqBody>): Promise<IApiResponse<ResData>> => {
        const response = await fetch(request.url, {
            method: 'DELETE',
            headers: request.headers,
        });
        const data = await response.json();
        return { data, status: response.status } as IApiResponse<ResData>;
    }
}

class WebDriver implements IDriver {
    public Api: IApi;
    constructor() {
        this.Api = new Api();
    }

    isDriver(): boolean {
        if (typeof window["fetch"] === 'function') {
            return true;
          }
        return false;
    }
    async isDriverAsync(): Promise<boolean> {
        return this.isDriver();
    }
}

export default WebDriver;
