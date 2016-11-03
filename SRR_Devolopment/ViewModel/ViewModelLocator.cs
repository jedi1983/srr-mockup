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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SRR_Devolopment.BaseLib.Class;
using System.Reflection;
using System;


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
        /// <summary>
        /// Member View Model Class
        /// </summary>
        public MemberViewModel Member
        {
            get
            {
                //checking whether already Contains/Object Created for this class?, if yes then unreg it first to clean cache up
                //and register back to the SimpleIoc dan let the service locator find the Object GodSpeed
                if (SimpleIoc.Default.ContainsCreated<MemberViewModel>() == true)
                {
                   
                    SimpleIoc.Default.Unregister<MemberViewModel>();
                    SimpleIoc.Default.Register<MemberViewModel>();
                    return ServiceLocator.Current.GetInstance<MemberViewModel>();

                }
                else//first run will let the service locator got the Object
                {
                    return ServiceLocator.Current.GetInstance<MemberViewModel>();
                }
                //return ServiceLocator.Current.GetInstance<MemberViewModel>();
            }
        }

      
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            
           
        }

    }
}