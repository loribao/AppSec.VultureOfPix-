export interface IApiResponse<B> {
    data: B;
    status: number;
    statusText: string;
    Error: IApiError;
}
export interface IApiRequest<R> {
    body: R | void;
    headers: IApiHeaders;
    url: string;
}
export interface IApiError {
    code: number;
    message: string | null;
    trace: string | null;
}
export interface IApiHeaders {
    [key: string]: string;
}
export interface IApi {
    get: <ResData>(request: IApiRequest<void>) => Promise<IApiResponse<ResData>>;
    post: <ReqBody, ResData> (request: IApiRequest<ReqBody>) => Promise<IApiResponse<ResData>>;
    put: <ReqBody,ResData>(request: IApiRequest<ReqBody>) => Promise<IApiResponse<ResData>>;
    delete: <ReqBody,ResData>(request: IApiRequest<ReqBody>) => Promise<IApiResponse<ResData>>;
}

export default interface IContext {
    Api: IApi;
}
