/* Zed Attack Proxy (ZAP) and its related class files.
 *
 * ZAP is an HTTP/HTTPS proxy for assessing web application security.
 *
 * Copyright 2019 the ZAP development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * http://localhost:8090/JSON/reports/action/generate/?apikey=ehmkp1au9qf3lu0rdn1fsa55rf&title=report&template=traditional-json-plus&theme=&description=&contexts=&sites=&sections=&includedConfidences=&includedRisks=&reportFileName=&reportFileNamePattern=&reportDir=&display=
 */

 use std::{thread, time};
 use zap_api::ZapApiError;
 use zap_api::ZapService;

 pub fn zap_api(zap_url: String,zap_api_key: String,target_url: String ) -> Result<(), ZapApiError> {
     /*
      * These examples assume:
      *    ZAP is running on http://localhost:8090
      *    ZAP has an API key of "ChangeMe" (or its disabled)
      *    Theres a suitable target app listening on localhost:8080
      */
     // You will need a ZAP instance running for these API calls to work.
     // Change the URL if your ZAP instance is listening on another host / port and the API key if you are using one.



     let service = ZapService {
         url: zap_url,
         api_key: zap_api_key,

     };

     // TODO run active scanner
     let value = zap_api::core::htmlreport(&service);
     let _value_sucess =   match value {
            Ok(v) => println!("{}", v),
            Err(e) => return Err(e),
        };
     // TODO display alerts

     Ok(())
 }

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        let zap_url = "http://localhost:8080".to_string();
        let zap_api_key = "7mv73vnotets5uj01cmt4nbjts".to_string();
        let target_url = "https://www.pudim.com.br".to_string();
        let result = zap_api(zap_url,zap_api_key,target_url);
        assert_eq!(result.is_ok(), true);
    }
}
