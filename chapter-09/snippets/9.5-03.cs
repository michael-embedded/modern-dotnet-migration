// Modern .NET, Chapter 9, Section 9.5
app.MapGet("/docs", () => Results.Content("""
<!doctype html>
<html><head><title>ShopApi</title>
<link rel="stylesheet" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css"></head>
<body><div id="ui"></div>
<script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js"></script>
<script>SwaggerUIBundle({ url: '/openapi/v1.json', dom_id: '#ui' });</script>
</body></html>
""", "text/html"));
