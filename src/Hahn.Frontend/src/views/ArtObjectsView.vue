<template>
  <v-container>
    <v-row class="my-5">
      <v-col cols="12">
        <h1 class="text-center">Art Objects</h1>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" md="6">
        <v-select
          v-model="selectedDepartment"
          :items="departmentOptions"
          item-value="departmentId"
          item-title="displayName"
          label="Select Department"
          outlined
        ></v-select>
      </v-col>
      <v-col cols="12" md="6" class="d-flex align-center">
        <v-btn @click="fetchArtObjects" color="primary" class="ml-4">
          Search
        </v-btn>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-data-table
          :headers="headers"
          :items="artObjects"
          class="elevation-1"
          item-value="objectID"
          no-data-text="No art objects found."
          v-model:items-per-page="pageSize"
          v-model:page="currentPage"
        ></v-data-table>
      </v-col>
    </v-row>

    <v-row class="my-4 justify-center">
      <v-pagination
        v-model="currentPage"
        :length="totalPages"
        @update:modelValue="handlePageChange"
      ></v-pagination>
    </v-row>
  </v-container>
</template>

<script>
import apiClient from "@/services/axios";

export default {
  name: "ArtObjectsView",
  data() {
    return {
      departments: [],
      selectedDepartment: null,
      artObjects: [],
      currentPage: 1,
      pageSize: 10,
      totalPages: 0,
      headers: [
        { text: "Title", value: "title" },
        { text: "Artist", value: "artistDisplayName" },
        { text: "Medium", value: "medium" },
        { text: "Dimensions", value: "dimensions" },
      ],
    };
  },
  created() {
    this.fetchDepartments();
  },
  watch: {
    pageSize(newValue, oldValue) {
      if (newValue !== oldValue) {
        this.currentPage = 1;
        this.fetchArtObjects();
      }
    },
  },
  methods: {
    async fetchDepartments() {
      try {
        const response = await apiClient.get("/museum/departments");
        console.log("Fetched Departments:", response.data);
        this.departments = JSON.parse(JSON.stringify(response.data));
        console.log("Mapped Departments:", this.departments);
      } catch (error) {
        console.error("Failed to fetch departments:", error);
      }
    },
    async fetchArtObjects() {
      if (!this.selectedDepartment) {
        alert("Please select a department.");
        return;
      }

      try {
        console.log("Fetching art objects for page:", this.currentPage);
        const response = await apiClient.get("/museum/art-objects", {
          params: {
            departmentId: this.selectedDepartment,
            page: this.currentPage,
            pageSize: this.pageSize,
          },
        });

        this.artObjects = response.data;
        this.totalPages = Math.ceil(
          response.headers["x-total-count"] / this.pageSize
        );
      } catch (error) {
        console.error("Failed to fetch art objects:", error);
      }
    },
    handlePageChange(newPage) {
      console.log("Page changed to:", newPage);
      this.currentPage = newPage;
      this.fetchArtObjects();
    },
  },
  computed: {
    departmentOptions() {
      return this.departments.map((dep) => ({
        departmentId: dep.departmentId,
        displayName: dep.displayName,
      }));
    },
  },
};
</script>

<style scoped>
.text-center {
  text-align: center;
}
</style>
