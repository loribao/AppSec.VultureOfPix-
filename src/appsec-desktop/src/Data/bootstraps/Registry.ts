import { registry } from 'tsyringe';
import Context from '../contexts/Context';
import LoginRepository from '../repositorys/LoginRepository';
import LoginCommand from '../../Domain/commands/loginCommand/LoginCommand';
import Store from '../stores/Store';

@registry([
    {token: 'store' , useClass: Store},
    {token: 'IContext' , useClass: Context},
    {token: 'ILoginRepository' , useClass: LoginRepository},
    {token: 'ILoginCommand' , useClass: LoginCommand},
])
class Registry{

}

export default Registry;
