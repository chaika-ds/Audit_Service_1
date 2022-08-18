function Create-ELK-Template{
    param (
        $templateName,
        $templateFile
    )
		
	$dir = "templates/"
    $url = 'http://localhost:9200/_template/' + $templateName;
    $filePath = Join-Path $dir -ChildPath $templateFile
    $body = Get-Content $filePath | Out-String
    Invoke-RestMethod -Method Put -Uri $url -ContentType 'application/json' -Body $body -ErrorAction Stop | Out-Null
}

$templates = [PSCustomObject]@{
    TemplateName = "aus-balance-losses-log"
    TemplateFile = "losses-log.json"
},
[PSCustomObject]@{
    TemplateName = "aus-system-auditlog2"
    TemplateFile = "audit.json"
},
[PSCustomObject]@{
    TemplateName = "aus-sso-blocked-players-log"
    TemplateFile = "blocked-players.json"
},
[PSCustomObject]@{
    TemplateName = "aus-system-player-changes-log"
    TemplateFile = "player-changes-log.json"
},
[PSCustomObject]@{
    TemplateName = "aus-sso-visit-log"
    TemplateFile = "visit-log.json"
};

foreach ($template in $templates)
{
 Create-ELK-Template -templateName $template.TemplateName -templateFile $template.TemplateFile
}

