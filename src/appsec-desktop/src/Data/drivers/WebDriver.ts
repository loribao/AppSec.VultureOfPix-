import { IApi, IApiRequest, IApiResponse, IStorage } from "../../Domain/interfaces/IContexts/IContext";
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

class Store implements IStorage{

    private storage: [{
        id: number;
        key: string;
        value: any;
    }];
    constructor() {
        this.storage = [{
            id: 0,
            key: '',
            value: ''
        }];
    }
    loadStorage(){
        const _storage_raw = localStorage.getItem('store');
        return _storage_raw ? JSON.parse(_storage_raw) : [];
    }
    async loadStorageAsync(): Promise<any> {
        const _storage_raw = localStorage.getItem('store');
        this.storage = _storage_raw ? JSON.parse(_storage_raw) : [];
        return this.storage ;
    }
   async save<T>(id:number,key: string,value:T) {
        id = id == 0 ? this.storage.length + 1 : id;
        this.storage.push({id, key, value});
        localStorage.setItem('store', JSON.stringify(this.storage));
    }
    async delete(key: string) {
        let d = this.storage.map((s) => s.key !== key);
        localStorage.setItem('store', JSON.stringify(d));
    }
    async get<T>(key: string): Promise<T> {
        let d = this.storage.find((s) => s.key === key)??null;
        localStorage.setItem('store', JSON.stringify(this.storage));
        return d as T;
    }
}

class WebDriver implements IDriver {
    public Api: IApi;
    public Store: IStorage;
    constructor() {
        this.Api = new Api();
        this.Store = new Store();
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
