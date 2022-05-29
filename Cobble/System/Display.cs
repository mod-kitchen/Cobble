using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;

#if DESKTOP
using OpenGL = Silk.NET.OpenGL;
using GLImGui = Silk.NET.OpenGL.Extensions.ImGui;
#elif ANDROID
using OpenGL = Silk.NET.OpenGLES;
using GLImGui = Silk.NET.OpenGLES.Extensions.ImGui;
#endif


namespace Cobble.System
{
    public class Display
    {
        private IWindow Window;
        private IView View;
        private OpenGL.GL Gl;
        private IInputContext InputContext;
        private GLImGui.ImGuiController ImGui;
        public bool HasWindow => Window != null;
        public bool HasDevTools => (Game.Platform == Platform.WINDOWS || Game.Platform == Platform.LINUX || Game.Platform == Platform.MAC);

        public Display()
        {
            if (Silk.NET.Windowing.Window.IsViewOnly)
            {
                var options = ViewOptions.Default;
                options.API = new GraphicsAPI(ContextAPI.OpenGLES, ContextProfile.Compatability, ContextFlags.Default, new APIVersion(3, 0));
                View = Silk.NET.Windowing.Window.GetView(options);
            } else
            {
                var options = WindowOptions.Default;
                options.API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, ContextFlags.Default, new APIVersion(3, 0));
                options.Title = Game.Title;
                Window = Silk.NET.Windowing.Window.Create(WindowOptions.Default);
                View = Window;
            }
            View.Load += OnLoad;
            View.FramebufferResize += OnResize;
            View.Update += OnUpdate;
            View.Render += OnRender;
            View.Closing += OnClose;
            View.Run();
        }

        private void OnClose()
        {
            Gl.Dispose();
        }

        private void OnRender(double delta)
        {
            Gl.ClearColor(Color.FromArgb(255, (int)(.45f * 255), (int)(.55f * 255), (int)(.60f * 255)));
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            //dev tools last as they need to overlay everything
            //todo: toggle hotkey
            if (HasDevTools) OnRenderDevTools(delta);
        }

        private void OnRenderDevTools(double delta)
        {
            ImGui.Update((float)delta);

            //do utility rendering here
            //for now just the demo window
            ImGuiNET.ImGui.ShowDemoWindow();

            ImGui.Render();
        }

        private void OnUpdate(double delta)
        {
            
        }

        private void OnResize(Silk.NET.Maths.Vector2D<int> size)
        {
            Gl.Viewport(size);
        }

        private void OnLoad()
        {
            Gl = OpenGL.GL.GetApi(View);
            InputContext = View.CreateInput();
            if(HasDevTools) ImGui = new GLImGui.ImGuiController(Gl, View, InputContext);
        }
    }
}
