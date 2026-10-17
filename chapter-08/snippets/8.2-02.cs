// Modern .NET, Chapter 8, Section 8.2
app.UseExceptionHandler();      // outermost: catches errors from everything below
app.UseHsts();                  // security headers (production)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();               // implicit in minimal hosting, explicit when you need to place things between
app.UseCors();
app.UseRateLimiter();
app.UseAuthentication();        // who are you?
app.UseAuthorization();         // may you?
// endpoints: app.MapControllers(), app.MapGet(...)
