using AppSec.Infra.Data.DataAcess.SonarQube.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSec.Infra.Data.DataAcess.SonarQube.Interfaces
{
    public interface IMeasuresComponent
    {
        [Get("/api/measures/component?additionalFields=period,metrics&component={projectName}&metricKeys=alert_status,quality_gate_details,new_violations,accepted_issues,new_accepted_issues,high_impact_accepted_issues,maintainability_issues,reliability_issues,security_issues,bugs,new_bugs,reliability_rating,new_reliability_rating,vulnerabilities,new_vulnerabilities,security_rating,new_security_rating,security_hotspots,new_security_hotspots,security_hotspots_reviewed,new_security_hotspots_reviewed,security_review_rating,new_security_review_rating,code_smells,new_code_smells,sqale_rating,new_maintainability_rating,sqale_index,new_technical_debt,coverage,new_coverage,lines_to_cover,new_lines_to_cover,tests,duplicated_lines_density,new_duplicated_lines_density,duplicated_blocks,ncloc,ncloc_language_distribution,projects,lines,new_lines")]
        public Task<MeasuresComponentPeriod> GetMeasuresComponentPerPeriod(string projectName);
    }
}
