import type { IApiRequest } from "../../Domain/interfaces/IContexts/IContext";
import { inject, injectable } from "tsyringe";
import ILoginRepository from "../../Domain/interfaces/IRepositories/ILoginRepository";
import type IContext from "../../Domain/interfaces/IContexts/IContext";
import Store from "../stores/Store";

@injectable()
class LoginRepository implements ILoginRepository {
    constructor(@inject("IContext") private driver: IContext, @inject("store") private store: Store) {}



    login = async (username: string, password: string): Promise<string> => {
        let query = {"query": `mutation{login(user:"${username}",pass:"${password}")}`};

        let url = new URL("/graphql",this.store.baseUrlBackend)
        let request: IApiRequest<any> = {
            url: url.toString(),
            body: query,
            headers: {
                "Content-Type": "application/json"
            }
        }

        let { status, data } = await this.driver.Api.post<string, {data: {login: string};}>(request);
        if (status >= 200 && status < 400) {
            return data.data.login;
        }
        else {
            throw new Error("Error on authenticate");
        }
    }
    logout = async (): Promise<boolean> => {
        throw new Error("Method not implemented.");
    }
}

export default LoginRepository;
