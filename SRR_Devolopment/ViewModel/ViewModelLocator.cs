/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:SRR_Devolopment.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SRR_Devolopment.Model;
using SRR_Devolopment.Services;

namespace SRR_Devolopment.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
    
            SimpleIoc.Default.Register<IDataServices, DataAccessServices>();
           
            SimpleIoc.Default.Register<MainViewModel>();
            
            SimpleIoc.Default.Register<IMemberDataService, MemberDataService>();

            SimpleIoc.Default.Register<MemberViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]

        
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public MemberViewModel Member
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MemberViewModel>();
            }
        }

      
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            
            //if(SimpleIoc.Default.GetInstance<MemberViewModel>()!=null)
            //{
            //    SimpleIoc.Default.Unregister<MemberViewModel>();
            //    SimpleIoc.Default.Register<MemberViewModel>();
            //}

        }

        /// <summary>
        /// Static Method Overload of Cleanup @Roland 2016
        /// </summary>
        /// <param name="ViewModelName">String PArameter ViewModelName</param>
        public static void Cleanup(string ViewModelName)
        {
            
        }
    }
}